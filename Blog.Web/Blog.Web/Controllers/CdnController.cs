using Blog.Models;
using Blog.Web.Filters;
using Blog.Web.ViewModels.Cdn;
using Common;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class CdnController : BaseController
    {
        public CdnController(BlogControllerContext context)
            : base(context)
        {
            var connectionStringBuilder = new StringBuilder("DefaultEndpointsProtocol=https;");
            connectionStringBuilder.Append("AccountName=").Append(context.CdnAccountName).Append(';');
            connectionStringBuilder.Append("AccountKey=").Append(context.CdnAccessKey).Append(';');
            CdnConnectionString = connectionStringBuilder.ToString();
        }

        protected readonly string CdnConnectionString;

        protected CloudBlobClient GetCdnClient()
        {
            var storageAccount = CloudStorageAccount.Parse(CdnConnectionString);
            var client = storageAccount.CreateCloudBlobClient();
            return client;
        }

        [HttpGet, BlogAuthorize(PermissionEnum.Admin)]
        public ActionResult List()
        {
            var client = GetCdnClient();

            var containers = client.ListContainers();
            var result = new ListViewModel
            {
                Containers = containers.Select(a => new ContainerLineItem
                {
                    Name = a.Name,
                    Uri = a.Uri.ToString()
                }).ToList()
            };

            return View(result);
        }

        [HttpPost, BlogAuthorize(PermissionEnum.Admin)]
        public ActionResult NewContainer(string containerName)
        {
            var client = GetCdnClient();

            containerName = containerName.Trim();
                        
            var containerReference = client.GetContainerReference(containerName);
            if(containerReference.Exists())
            {
                TempData.StoreNotification(new ViewModels.Feed.Notification
                {
                    Type = ViewModels.Feed.NotificationType.Error,
                    Subject = "Directory Already Exists",
                    Message = "That directory already exists, please try again"
                });

                return RedirectToAction("List");
            }

            containerReference.CreateIfNotExists();
            containerReference.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            TempData.StoreNotification(new ViewModels.Feed.Notification
            {
                Type = ViewModels.Feed.NotificationType.Confirmation,
                Subject = "Success",
                Message = "Directory successfully created"
            });
            return RedirectToAction("List");
        }

        [HttpGet, BlogAuthorize(PermissionEnum.Admin)]
        public ActionResult Resources(string directory)
        {
            var client = GetCdnClient();
            var container = client.GetContainerReference(directory);

            var result = new ResourcesViewModel
            {
                Directory = directory,
                LineItems = container.ListBlobs().Select(a => new ResourceLineItem
                {
                    Name = ((ICloudBlob)a).Name,
                    Url = a.Uri.ToString()
                }).ToList()
            };
            return View(result);
        }

        [HttpGet, BlogAuthorize(PermissionEnum.Admin)]
        public ActionResult NewResource(string directory)
        {
            return View(new NewResourceViewModel
                {
                    Directory = directory
                });
        }

        [HttpPost, BlogAuthorize(PermissionEnum.Admin)]
        public ActionResult NewResource(string directory, HttpPostedFileBase file)
        {
            var client = GetCdnClient();
            var container = client.GetContainerReference(directory);

            var fileName = Path.GetFileName(file.FileName);

            // Get existing or create new
            var blobReference = container.GetBlockBlobReference(fileName);
            var stream = new MemoryStream();
            file.InputStream.CopyTo(stream);

            // WHY ARE STREAMS SO STUPID?!
            stream.Position = 0;

            // This overwrites
            blobReference.UploadFromStream(stream);
            
            return RedirectToAction("Resources", new { directory = directory });
        }

        [HttpGet, BlogAuthorize(PermissionEnum.Admin)]
        public ActionResult DeleteResource(string directory, string fileName)
        {
            var client = GetCdnClient();
            var container = client.GetContainerReference(directory);
            
            // Get existing or create new
            var blobReference = container.GetBlockBlobReference(fileName);
            blobReference.Delete(DeleteSnapshotsOption.IncludeSnapshots);

            return RedirectToAction("Resources", new { directory = directory });
        }
    }
}

using Blog.Web.ViewModels.Account;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(BlogControllerContext context)
            : base(context) { }

        public void SendVerificationEmail()
        {
            //var from = new MailAddress("noreply@eouw0o83hf.com");
            //var to = new[] { new MailAddress("nathanlandis@gmail.com") };
            //var subject = "Testing SendGrid!";
            //var html = @"<h1>Oh hai!</h1><p>Here's where the body would be, along with a</p><h2><a href=""#"">Confirmation Link</a></h2>";

            //var message = SendGrid.Mail.GetInstance(from, to, new MailAddress[0], new MailAddress[0], subject, html, null);
            //var creds = new NetworkCredential(Context.SendGridUsername, Context.SendGridPassword);
            //var transport = SendGrid.Transport.SMTP.GetInstance(creds);
            //transport.Deliver(message);
        }

        //[HttpGet]
        //public ActionResult UploadImage()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult UploadImageOtherThing(HttpPostedFileBase file)
        //{
        //    if (file.ContentLength == 0)
        //    {
        //        throw new Exception();
        //    }

        //    var connectionStringBuilder = new StringBuilder("DefaultEndpointsProtocol=https;");
        //    connectionStringBuilder.Append("AccountName=").Append(ConfigurationManager.AppSettings["Cdn_AccountName"]).Append(';');
        //    connectionStringBuilder.Append("AccountKey=").Append(ConfigurationManager.AppSettings["Cdn_AccessKey_Primary"]).Append(';');

        //    var storageAccount = CloudStorageAccount.Parse(connectionStringBuilder.ToString());
        //    var client = storageAccount.CreateCloudBlobClient();

        //    var imagesContainer = ConfigurationManager.AppSettings["Cdn_ImageContainer"];
        //    var containerReference = client.GetContainerReference(imagesContainer);

        //    // Make sure it exists
        //    if (!containerReference.Exists())
        //    {
        //        containerReference.CreateIfNotExists();
        //        containerReference.SetPermissions(new BlobContainerPermissions
        //        {
        //            PublicAccess = BlobContainerPublicAccessType.Blob
        //        });
        //    }

        //    var name = Path.GetFileName(file.FileName);

        //    var blobReference = containerReference.GetBlockBlobReference(name);

        //    var stream = new MemoryStream();
        //    file.InputStream.CopyTo(stream);

        //    // Warning, this overwrites
        //    blobReference.UploadFromStream(stream);

        //    var result = new BlobUploadResultViewModel
        //    {
        //        UploadedUrl = blobReference.Uri.ToString(),
        //        AllBlobs = containerReference.ListBlobs().Select(a => a.Uri.ToString()).ToList()
        //    };

        //    return View("UploadImage", result);
        //}
    }
}

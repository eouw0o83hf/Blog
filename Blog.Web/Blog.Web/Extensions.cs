using Blog.Web.ViewModels.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web
{
    public static class Extensions
    {
        public static void StoreNotification(this TempDataDictionary tempData, Notification notification)
        {
            tempData["Notification"] = notification;
        }

        public static Notification GetNotification(this TempDataDictionary tempData)
        {
            return tempData["Notification"] as Notification;
        }
    }
}
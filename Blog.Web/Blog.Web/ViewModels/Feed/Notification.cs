using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.ViewModels.Feed
{
    public class Notification
    {
        public NotificationType Type { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }

    public enum NotificationType
    {
        Information,
        Confirmation,
        Warning,
        Error
    }
}
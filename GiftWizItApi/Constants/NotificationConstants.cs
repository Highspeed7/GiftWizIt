using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Constants
{
    public static class NotificationConstants
    {
        public const string ListCreatedNotifTitle = "Gift List Created";
        public const string ListShareFailedNotifTitle = "One or more gift list shares failed";
        public const string ListShareSuccessNotifTitle = "A list has been shared with you";

        public const string ContactAddedNotifTitle = "You've been added as someone's contact";
        public const string ContactDeleteSuccessNotifTitle = "Contact successfully deleted.";

        public const string InfoType = "info";
        public const string Success = "success";
        public const string WarningType = "warning";
    }
}

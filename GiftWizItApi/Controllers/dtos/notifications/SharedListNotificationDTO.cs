using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos.notifications
{
    public class SharedListNotificationDTO
    {
        public string NotificationTitle { get; set; }
        public string type = "ListShared";
    }
}

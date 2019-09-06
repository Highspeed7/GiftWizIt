using GiftWizItApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Services
{
    public class NotificationsService: INotificationsService
    {
        public string UserEmail { get; set; }
    }
}

using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class NotificationsRepository : Repository<Notifications>, INotificationsRepository
    {
        public NotificationsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

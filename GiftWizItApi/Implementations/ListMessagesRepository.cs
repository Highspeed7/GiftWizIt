using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class ListMessagesRepository : Repository<ListMessages>, IListMessagesRepository
    {
        public ListMessagesRepository(ApplicationDbContext context) : base(context){}
    }
}

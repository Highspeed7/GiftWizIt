using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class TagsRepository : Repository<Tags>, ITagsRepository
    {
        public TagsRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public new Tags Add(Tags tag)
        {
            base.Add(tag);
            return tag;
        }
    }
}

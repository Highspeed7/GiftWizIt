using AutoMapper;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class SharedListsRepository : Repository<SharedLists>, ISharedListsRepository
    {
        public SharedListsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public SharedLists AddSharedList(SharedLists sharedList)
        {
            base.Add(sharedList);

            // I think this will return the giftlist associated with the share.
            Context.Entry(sharedList).Reference(sl => sl.GiftList).Load();

            return sharedList;
        }

        //public SharedLists AddSharedList(GListShareDTO sharedList)
        //{
        //    //SharedLists listToShare = new SharedLists();
        //    //foreach(ContactDTO contact in sharedList.Contacts)
        //    //{
        //    //    listToShare.GiftListId = sharedList.G_List_Id;
        //    //    listToShare.Password = sharedList.Password;
        //    //    listToShare.Contact = mapper.Map<Contacts>(contact);

        //    //    base.Add(listToShare);
        //    //}
        //    //return listToShare;
        //}
    }
}

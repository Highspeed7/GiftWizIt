using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context )
        {
            _context = context;
            GiftLists = new GiftListRepository(_context);
            GiftItems = new GiftItemRepository(_context);
            Users = new UserRepository(_context);
            WishLists = new WishListRepository(_context);
            WishItems = new WishItemRepository(_context);
            LnksItmsPtns = new LnksItmsPtnrsRepository(_context);
            Partners = new PartnersRepository(_context);
            Contacts = new ContactRepository(_context);
            ContactUsers = new ContactUsersRepository(_context);
        }

        public IGiftListRepository GiftLists { get; private set; }
        public IGiftItemRepository GiftItems { get; private set; }
        public IUserRepository Users { get; private set; }
        public IWishListRepository WishLists { get; private set; }
        public IWishItemRepository WishItems { get; private set; }
        public ILnksItmsPtnrsRepository LnksItmsPtns { get; private set; }
        public IPartnersRepository Partners { get; private set; }
        public IContactRepository Contacts { get; private set; }
        public IContactUsersRepository ContactUsers { get; private set; }

        public async Task<int> CompleteAsync()
        {
            var res = await _context.SaveChangesAsync();
            return res;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

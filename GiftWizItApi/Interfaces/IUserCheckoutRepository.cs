﻿using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IUserCheckoutRepository: IRepository<UserCheckout>
    {
        Task<IEnumerable<UserCheckout>> GetUserCheckout(string userId);
    }
}

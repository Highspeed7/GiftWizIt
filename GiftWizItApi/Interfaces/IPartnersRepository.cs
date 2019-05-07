using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IPartnersRepository: IRepository<Partners>
    {
        void Add(string name, string domain);
        Task<IEnumerable<Partners>> GetPartnerAsync(string domain);
    }
}

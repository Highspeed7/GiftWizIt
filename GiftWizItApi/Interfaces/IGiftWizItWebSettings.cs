using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IGiftWizItWebSettings
    {
        string LocalBaseUrl { get; }
        string ProdBaseUrl { get; }
    }
}

using GiftWizItApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class GiftWizItWebSettings : IGiftWizItWebSettings
    {
        public string LocalBaseUrl { get; set; }
        public string ProdBaseUrl { get; set; }
    }
}

using GiftWizItApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace GiftWizItApi.SignalR.Hubs
{
    public class MainHub: Hub
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}

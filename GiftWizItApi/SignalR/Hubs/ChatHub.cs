﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.SignalR.Hubs
{
    public class ChatHub: Hub
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}

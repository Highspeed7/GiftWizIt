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
    public class NotificationsHub: Hub
    {
        //public static ConcurrentDictionary<string, List<string>> ConnectedUsers = new ConcurrentDictionary<string, List<string>>();
        //private readonly INotificationsService notifSvc;

        //public NotificationsHub(
        //    INotificationsService notifSvc)
        //{
        //    this.notifSvc = notifSvc;
        //}

        //public override Task OnConnectedAsync()
        //{
        //    var userEmail = this.notifSvc.UserEmail != null ? notifSvc.UserEmail : "NoEmail";

        //    if(userEmail == "NoEmail")
        //    {
        //        Context.Abort();
        //    }
        //    List<string> existingUserConnectionIds;

        //    ConnectedUsers.TryGetValue(userEmail, out existingUserConnectionIds);

        //    if(existingUserConnectionIds == null )
        //    {
        //        existingUserConnectionIds = new List<string>();
        //    }

        //    existingUserConnectionIds.Add(Context.ConnectionId);

        //    ConnectedUsers.TryAdd(userEmail, existingUserConnectionIds);

        //    return base.OnConnectedAsync();
        //}

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    var userEmail = this.notifSvc.UserEmail != null ? notifSvc.UserEmail : "NoEmail";

        //    List<string> existingUserConnectionIds;
        //    if(!ConnectedUsers.TryGetValue(userEmail, out existingUserConnectionIds))
        //    {
        //        ConnectedUsers.TryGetValue("NoEmail", out existingUserConnectionIds);
        //    }

        //    // remove the connection id from the List 
        //    existingUserConnectionIds.Remove(Context.ConnectionId);

        //    // If there are no connection ids in the List, delete the user from the global cache (ConnectedUsers).
        //    if (existingUserConnectionIds.Count == 0)
        //    {
        //        // if there are no connections for the user,
        //        // just delete the userName key from the ConnectedUsers concurent dictionary
        //        List<string> garbage; // to be collected by the Garbage Collector
        //        ConnectedUsers.TryRemove(userEmail, out garbage);
        //    }

        //    return base.OnDisconnectedAsync(exception);
        //}

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}

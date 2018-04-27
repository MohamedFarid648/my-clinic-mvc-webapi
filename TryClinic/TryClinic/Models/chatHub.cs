using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace TryClinic.Models
{
    public class chatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        //1.sendMessage that client will call it 
        public void sendMessage(string sender,string reciever, string msg)
        {

            string groupName = $"{sender}_{reciever}_group";
            Clients.All.newMessage(sender,reciever, msg, groupName);//Fire Event newMessage that client will create it
            //Clients.User(reciever).newMessage(sender, reciever, msg, groupName);//Fire Event newMessage that client will create it
        }
    }
}
using System.Collections.Generic;
using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Data.Entity;
using System.Linq;
using RiderQc.Web.ViewModels.Api.Message;
using System;

namespace RiderQc.Web.DAL
{
    public class MessageDao : IMessageDao
    {
        public List<Message> GetInboxMesages(string me, int fetchNb)
        {
            List<Message> messages = new List<Message>();

            using (RiderQcContext ctx = new RiderQcContext())
            {
                User _me = ctx.Users.FirstOrDefault(x => x.Username == me);

                if (_me != null)
                {
                    messages =
                        ctx.Messages
                        .Include(x => x.Sender)
                        .Include(x => x.Receiver)
                        .Where(x => x.ReceiverId == _me.UserID).OrderBy(y => y.TimeStamp).ToList();
                }
            }
            return messages;
        }

        public List<Message> GetOutboxMesages(string me, int fetchNb)
        {
            List<Message> messages = new List<Message>();

            using (RiderQcContext ctx = new RiderQcContext())
            {
                User _me = ctx.Users.FirstOrDefault(x => x.Username == me);

                if (_me != null)
                {
                    messages =
                        ctx.Messages
                        .Include(x => x.Sender)
                        .Include(x => x.Receiver)
                        .Where(x => x.SenderId == _me.UserID).OrderBy(y => y.TimeStamp).ToList();
                }
            }
            return messages;
        }

        public List<Message> ConversationMessageList(string me, string receiver)
        {
            List<Message> messages = new List<Message>();

            using (RiderQcContext ctx = new RiderQcContext())
            {
                User _me = ctx.Users.FirstOrDefault(x => x.Username == me);
                User _receiver = ctx.Users.FirstOrDefault(x => x.Username == receiver);

                if(_me != null && _receiver != null )
                {
                    messages = 
                        ctx.Messages
                        .Include(x => x.Sender)
                        .Include(x => x.Receiver)
                        .Where(x => x.SenderId == _me.UserID && x.ReceiverId == _receiver.UserID).OrderBy(y => y.TimeStamp).ToList();
                }
            }

            return messages;
        }

        public int SendMessage(SendMessageViewModel messageViewModel)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                User me = ctx.Users.FirstOrDefault(x => x.Username == messageViewModel.Me);
                User receiver = ctx.Users.FirstOrDefault(x => x.Username == messageViewModel.Receiver);

                if (me != null && receiver != null)
                {
                    Message _message = new Message();
                    _message.MessageText = messageViewModel.MessageText;
                    _message.Read = null;
                    _message.Sender = me;
                    _message.Receiver = receiver;
                    _message.TimeStamp = DateTime.Now;
                    
                    Message msg = ctx.Messages.Add(_message);
                    ctx.SaveChanges();

                    return msg?.MessageId > 0 ? msg.MessageId : -1;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
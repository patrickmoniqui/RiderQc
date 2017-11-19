using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Api.Message;
using RiderQc.Web.ViewModels.User;
using System;
using System.Collections.Generic;

namespace RiderQc.Web.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMessageDao dao;

        public MessageRepository(IMessageDao _dao)
        {
            dao = _dao;
        }

        public List<MessageConversationViewModel> ConversationMessageList(string me, string receiver)
        {
            List<MessageConversationViewModel> messages = new List<MessageConversationViewModel>();

            List<Message> _messages = dao.ConversationMessageList(me, receiver);

            foreach (Message message in _messages)
            {
                MessageConversationViewModel msg = new MessageConversationViewModel();

                msg.Me = message.Sender.Username == me ? true : false;
                msg.MessageId = message.MessageId;
                msg.MessageText = message.MessageText;
                msg.Read = message.Read;
                msg.TimeStamp = DateTime.Now;

                UserSimpleViewModel _me = new UserSimpleViewModel();
                _me.UserID = message.Sender.UserID;
                _me.Username = message.Sender.Username;

                UserSimpleViewModel _receiver = new UserSimpleViewModel();
                _receiver.UserID = message.Receiver.UserID;
                _receiver.Username = message.Receiver.Username;

                msg.Sender = _me;
                msg.Receiver = _receiver;

                messages.Add(msg);
            }

            return messages;
        }

        public int SendMessage(SendMessageViewModel messageViewModel)
        {
            int messageId = dao.SendMessage(messageViewModel);

            return messageId;
        }
    }
}
using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Api.Message;
using System.Collections.Generic;

namespace RiderQc.Web.DAL.Interface
{
    public interface IMessageDao
    {
        List<Message> GetOutboxMesages(string me, int fetchNb);
        List<Message> GetInboxMesages(string me, int fetchNb);
        int SendMessage(SendMessageViewModel message);
        List<Message> ConversationMessageList(string user, string receiver);
    }
}

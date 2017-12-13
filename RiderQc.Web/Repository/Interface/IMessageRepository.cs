using RiderQc.Web.ViewModels.Api.Message;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface IMessageRepository
    {
        List<InboxMessageViewModel> GetInboxMessages(string me, int fetchNb = -1);
        List<OutboxMessageViewModel> GetOutboxMessages(string me, int fetchNb = -1);
        int SendMessage(SendMessageViewModel messageViewModel);
        List<MessageConversationViewModel> ConversationMessageList(string me, string receiver);
    }
}

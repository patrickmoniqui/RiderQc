using RiderQc.Web.ViewModels.Api.Message;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface IMessageRepository
    {
        int SendMessage(SendMessageViewModel messageViewModel);
        List<MessageConversationViewModel> ConversationMessageList(string me, string receiver);
    }
}

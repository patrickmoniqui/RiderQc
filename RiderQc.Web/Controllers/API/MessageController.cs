using RiderQc.Web.App_Start;
using RiderQc.Web.Models;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Api.Message;
using System.Collections.Generic;
using System.Web.Http;

namespace RiderQc.Web.Controllers.API
{
    [RoutePrefix("message")]
    public class MessageController : ApiController
    {
        private readonly IMessageRepository repo;

        public MessageController(IMessageRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Send Message to a user.
        /// </summary>
        /// <param name="messageViewModel"></param>
        /// <returns>Message Id</returns>
        [AuthTokenAuthorization]
        [HttpPost]
        [Route("send")]
        public IHttpActionResult SendMessage(SendMessageViewModel messageViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = (ApplicationUser)User;

            messageViewModel.Me = user.Username;

            int messageId = repo.SendMessage(messageViewModel);

            if(messageId > 0)
            {
                return Ok(messageId);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get receveived messages
        /// </summary>
        /// <param name="fetch"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpGet]
        [Route("message/inbox")]
        public IHttpActionResult GetInbox(int fetch = -1)
        {
            ApplicationUser user = (ApplicationUser)User;

            List<InboxMessageViewModel> messages = repo.GetInboxMessages(user.Username, fetch);

            if (messages.Count > 0)
            {
                return Ok(messages);
            }
            else if (messages.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get sent messages
        /// </summary>
        /// <param name="fetch"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpGet]
        [Route("message/outbox")]
        public IHttpActionResult GetOutbox(int fetch = -1)
        {
            ApplicationUser user = (ApplicationUser)User;

            List<OutboxMessageViewModel> messages = repo.GetOutboxMessages(user.Username, fetch);

            if (messages.Count > 0)
            {
                return Ok(messages);
            }
            else if (messages.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get messages from a conversation with a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpGet]
        [Route("conversation")]
        public IHttpActionResult GetMessagesWithUser([FromUri] string with, [FromUri] int nbMaxMessages = 0)
        {
            ApplicationUser user = (ApplicationUser)User;

            List<MessageConversationViewModel> messages = repo.ConversationMessageList(user.Username, with);

            if (messages.Count > 0)
            {
                return Ok(messages);
            }
            else if (messages.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

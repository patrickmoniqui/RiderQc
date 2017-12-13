using RiderQc.Web.App_Start;
using RiderQc.Web.Models;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Comment;
using System.Collections.Generic;
using System.Web.Http;

namespace RiderQc.Web.Controllers.API
{
    [RoutePrefix("comment")]
    public class CommentController : ApiController
    {
        private readonly ICommentRepository repo;
        private readonly IRideRepository rideRepo;

        public CommentController(ICommentRepository _repo, IRideRepository _rideRepo, ICommentRepository _commentRepo)
        {
            repo = _repo;
            rideRepo = _rideRepo;
        }

        /// <summary>
        /// Get Comment by Id
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{commentId}")]
        public IHttpActionResult GetById(int commentId, bool includeChildComments = true)
        {
            CommentViewModel commentViewModel;

            if(includeChildComments)
            {
                commentViewModel = repo.GetCommentAndChildCommentsById(commentId);
            }
            else
            {
                commentViewModel =repo.GetById(commentId);
            }

            if(commentViewModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(commentViewModel);
            }
        }

        /// <summary>
        /// Get Comments for User by Id
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("byuser/{userId}")]
        public IHttpActionResult GetCommentsByUserId(int userId)
        {
            List<CommentViewModel> commentsViewModel = repo.GetCommentsByUserId(userId);

            if (commentsViewModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(commentsViewModel);
            }
        }

        /// <summary>
        /// Reply to a comment
        /// </summary>
        /// <returns>Id of new comment</returns>
        [AuthTokenAuthorization]
        [HttpPost]
        [Route("reply")]
        public IHttpActionResult ReplyToComment(CommentReplyViewModel comment)
        {
            ApplicationUser user = (ApplicationUser)User;

            if(!ValidateComment(comment.ParentId) && !ValidateRide(comment.RideId))
            {
                ModelState.AddModelError("ParentId", "Parent Comment is not valid");
                ModelState.AddModelError("RideId", "Ride is not valid");
            }

            if (ModelState.IsValid)
            {
                CommentViewModel commentViewModel = new CommentViewModel();
                commentViewModel.CommentText = comment.CommentText;
                commentViewModel.ParentId = comment.ParentId;
                commentViewModel.RideId = comment.RideId;
                commentViewModel.SenderId = user.Id;

                int CommentId = repo.ReplyToComment(commentViewModel);

                if (CommentId > 0)
                {
                    return Ok(CommentId);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Delete a comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpDelete]
        [Route("{commentId}")]
        public IHttpActionResult DeleteComment(int commentId)
        {
            ApplicationUser user = (ApplicationUser)User;
            
            if(!repo.IsCreator(user.Id, commentId))
            {
                return BadRequest("You can not delete this comment.");
            }

            if (commentId <= 0)
            {
                return BadRequest("commmentId is invalid.");
            }

            bool result = repo.Delete(commentId);

            if (result)
            {
                return Ok("Comment successfully deleted");
            }
            else
            {
                 return BadRequest("Error while deleting comment.");
            }
        }

        private bool ValidateComment(int? commentId)
        {
            if (commentId != null && commentId <= 0 && !repo.Exist((int)commentId))
            {
                return false;
            }
            return true;
        }

        private bool ValidateRide(int? rideId)
        {
            if((rideId != null && rideId <= 0) && !rideRepo.Exist((int)rideId))
            {
                return false;
            }
            return true;
        }
    }
}

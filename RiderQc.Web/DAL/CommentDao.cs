using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using RiderQc.Web.ViewModels.Comment;
using RiderQc.Web.ViewModels.User;

namespace RiderQc.Web.DAL
{
    public class CommentDao : ICommentDao
    {
        public bool Delete(int commentId)
        {
            using (RiderQcContext context = new RiderQcContext())
            {
                Comment comment = context.Comments.FirstOrDefault(x => x.CommentId == commentId);
                context.Comments.Remove(comment);

                int result = context.SaveChanges();

                return result > 0 ? true : false;
            }
        }

        public bool Exist(int commentId)
        {
            using (RiderQcContext context = new RiderQcContext())
            {
                return context.Comments.Any(x => x.CommentId == commentId);
            }
        }

        public Comment GetSingleCommentById(int commentId)
        {
            if (commentId <= 0)
            {
                return null;
            }

            using (RiderQcContext context = new RiderQcContext())
            {
                return context.Comments.FirstOrDefault(x => x.CommentId == commentId);
            }
        }

        public CommentViewModel GetCommentAndChildCommentsById(int commentId)
        {
            if (commentId <= 0)
            {
                return null;
            }

            using (RiderQcContext context = new RiderQcContext())
            {
                Comment comment = context.Comments
                    .Include(x => x.User)
                    // include child comments of comment
                    .Include(x => x.ChildComments)
                    .Include(x => x.ChildComments.Select(y => y.User))
                    .FirstOrDefault(x => x.CommentId == commentId);

                CommentViewModel commentViewModel = CommentToCommentViewModel(comment);
                return commentViewModel;
            }
        }

        public List<Comment> GetCommentsByUserId(int userId)
        {
            if(userId <= 0)
            {
                return null;
            }

            using (RiderQcContext context = new RiderQcContext())
            {
                return context.Comments.Where(x => x.SenderId == userId).ToList();
            }
        }

        public bool IsCreator(int userId, int commentId)
        {
            using (RiderQcContext context = new RiderQcContext())
            {
                Comment comment = context.Comments.FirstOrDefault(x => x.CommentId == commentId);

                if (comment != null && comment.SenderId == userId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int ReplyToComment(Comment comment)
        {
            using (RiderQcContext context = new RiderQcContext())
            {
                Comment newComment = context.Comments.Add(comment);
                int result = context.SaveChanges();

                return result >= 1 ? newComment.CommentId : -1;
            }
        }

        private CommentViewModel CommentToCommentViewModel(Comment comment)
        {
            if(comment == null)
            {
                return null;
            }

            CommentViewModel commentViewModel = new CommentViewModel();
            commentViewModel.CommentId = comment.CommentId;
            commentViewModel.ParentId = comment.ParentId;
            commentViewModel.SenderId = comment.SenderId;
            commentViewModel.TimeStamp = comment.TimeStamp;
            commentViewModel.Vote = comment.Vote;
            commentViewModel.Blocked = comment.Blocked;
            commentViewModel.RideId = comment.RideId;

            commentViewModel.User = new UserViewModel() { UserID = comment.User.UserID, Username = comment.User.Username };
            commentViewModel.CommentText = comment.CommentText;
            commentViewModel.ChildComments = new List<CommentViewModel>();

            if (comment.ChildComments != null)
            {
                foreach (Comment comment2 in comment.ChildComments)
                {
                    commentViewModel.ChildComments.Add(CommentToCommentViewModel(comment2));
                }
            }

            return commentViewModel;
        }
    }
}
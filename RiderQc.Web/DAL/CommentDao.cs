using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System;
using System.Linq;

namespace RiderQc.Web.DAL
{
    public class CommentDao : ICommentDao
    {
        public bool Exist(int commentId)
        {
            using (RiderQcContext context = new RiderQcContext())
            {
                return context.Comments.Any(x => x.CommentId == commentId);
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
    }
}
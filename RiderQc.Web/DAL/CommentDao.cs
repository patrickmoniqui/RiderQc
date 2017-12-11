using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

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

        public Comment GetById(int commentId)
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
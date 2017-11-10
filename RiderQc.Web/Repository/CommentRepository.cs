using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Models;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Comment;
using System;

namespace RiderQc.Web.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ICommentDao dao;

        public CommentRepository(ICommentDao _dao)
        {
            dao = _dao;
        }

        public bool Exist(int commentId)
        {
            return dao.Exist(commentId);
        }

        public int ReplyToComment(CommentViewModel commentViewModel)
        {
            Comment comment = new Comment();
            comment.CommentText = commentViewModel.CommentText;
            comment.ParentId = commentViewModel.ParentId;
            comment.RideId = commentViewModel.RideId;
            comment.SenderId = commentViewModel.SenderId;
            comment.TimeStamp = DateTime.Now;

            int id = dao.ReplyToComment(comment);

            return id;
        }
    }
}
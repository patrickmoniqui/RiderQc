using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Models;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Comment;
using RiderQc.Web.ViewModels.User;
using System;
using System.Collections.Generic;

namespace RiderQc.Web.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ICommentDao dao;

        public CommentRepository(ICommentDao _dao)
        {
            dao = _dao;
        }

        public bool Delete(int commentId)
        {
            return dao.Delete(commentId);
        }

        public bool Exist(int commentId)
        {
            return dao.Exist(commentId);
        }

        public CommentViewModel GetById(int commentId)
        {
            Comment comment = dao.GetSingleCommentById(commentId);

            if(comment == null)
            {
                return null;
            }

            CommentViewModel commentViewModel = new CommentViewModel();
            commentViewModel.CommentId = comment.CommentId;
            commentViewModel.CommentText = comment.CommentText;
            commentViewModel.SenderId = comment.SenderId;
            commentViewModel.ParentId = comment.ParentId;
            commentViewModel.RideId = comment.RideId;
            commentViewModel.TimeStamp = comment.TimeStamp;
            commentViewModel.Vote = comment.Vote;
            commentViewModel.Blocked = comment.Blocked;

            return commentViewModel;
        }

        public CommentViewModel GetCommentAndChildCommentsById(int commentId)
        {
            CommentViewModel commentViewModel = dao.GetCommentAndChildCommentsById(commentId);

            if (commentViewModel == null)
            {
                return null;
            }
            
            return commentViewModel;
        }

        public List<CommentViewModel> GetCommentsByUserId(int userId)
        {
            List<Comment> comments = dao.GetCommentsByUserId(userId);

            if(comments == null)
            {
                return null;
            }

            List<CommentViewModel> commentsViewModel = new List<CommentViewModel>();

            foreach(Comment comment in comments)
            {
                CommentViewModel commentViewModel = new CommentViewModel();
                commentViewModel.CommentId = comment.CommentId;
                commentViewModel.CommentText = comment.CommentText;
                commentViewModel.SenderId = comment.SenderId;
                commentViewModel.ParentId = comment.ParentId;
                commentViewModel.RideId = comment.RideId;
                commentViewModel.TimeStamp = comment.TimeStamp;
                commentViewModel.Vote = comment.Vote;
                commentViewModel.Blocked = comment.Blocked;

                commentsViewModel.Add(commentViewModel);
            }

            return commentsViewModel;
        }

        public bool IsCreator(int userId, int commentId)
        {
            return dao.IsCreator(userId, commentId);
        }

        public int ReplyToComment(CommentViewModel commentViewModel)
        {
            Comment comment = new Comment();
            comment.CommentText = commentViewModel.CommentText;
            comment.SenderId = commentViewModel.SenderId;
            comment.ParentId = commentViewModel.ParentId;
            comment.RideId = commentViewModel.RideId;
            comment.TimeStamp = DateTime.Now;

            int id = dao.ReplyToComment(comment);

            return id;
        }
    }
}
﻿using RiderQc.Web.ViewModels.Comment;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface ICommentRepository
    {
        CommentViewModel GetById(int commentId);
        CommentViewModel GetCommentAndChildCommentsById(int commentId);
        bool Delete(int commentId);
        List<CommentViewModel> GetCommentsByUserId(int userId);
        bool Exist(int commentId);
        int ReplyToComment(CommentViewModel comment);
        bool IsCreator(int userId, int commentId);
    }
}
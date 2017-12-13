using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Comment;
using System.Collections.Generic;

namespace RiderQc.Web.DAL.Interface
{
    public interface ICommentDao
    {
        Comment GetSingleCommentById(int commentId);
        CommentViewModel GetCommentAndChildCommentsById(int commentId);
        List<Comment> GetCommentsByUserId(int userId);
        bool IsCreator(int userId, int commentId);
        bool Delete(int commentId);
        int ReplyToComment(Comment comment);
        bool Exist(int commentId);
    }
}

using RiderQc.Web.Entities;
using System.Collections.Generic;

namespace RiderQc.Web.DAL.Interface
{
    public interface ICommentDao
    {
        Comment GetById(int commentId);
        List<Comment> GetCommentsByUserId(int userId);
        bool Delete(int userId);
        int ReplyToComment(Comment comment);
        bool Exist(int commentId);
    }
}

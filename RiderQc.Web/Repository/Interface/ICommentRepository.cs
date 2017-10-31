using RiderQc.Web.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiderQc.Web.Repository.Interface
{
    public interface ICommentRepository
    {
        bool Exist(int commentId);
        int ReplyToComment(CommentViewModel comment);
    }
}
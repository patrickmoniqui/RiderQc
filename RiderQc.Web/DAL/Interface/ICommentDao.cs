using RiderQc.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiderQc.Web.DAL.Interface
{
    public interface ICommentDao
    {
        int ReplyToComment(Comment comment);
        bool Exist(int commentId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiderQc.Web.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int SenderId { get; set; }
        public Nullable<int> RideId { get; set; }
        public Nullable<int> TrajetId { get; set; }
        public string CommentText { get; set; }
        public bool Blocked { get; set; }
        public int Vote { get; set; }
        public System.DateTime TimeStamp { get; set; }

        public ICollection<RiderQc.Web.ViewModels.Comment.CommentViewModel> ChildComments { get; set; }
        public RiderQc.Web.ViewModels.User.UserViewModel User { get; set; }
    }
}
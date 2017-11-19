using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiderQc.Web.ViewModels.Comment
{
    public class CommentReplyViewModel
    {
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> RideId { get; set; }
        public string CommentText { get; set; }
    }
}
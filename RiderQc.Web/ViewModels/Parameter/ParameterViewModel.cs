using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiderQc.Web.ViewModels.Parameter
{
    public class ParameterViewModel
    {
       public List<SelectListItem> TrajetList { get; set; }
       public List<SelectListItem> UserList { get; set; }
       public List<SelectListItem> RideList { get; set; }
       public List<SelectListItem> LevelList { get; set; }
       public List<SelectListItem> CommentList { get; set; }

    }
}



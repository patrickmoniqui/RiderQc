using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.Ride
{
    public class TrajetViewModel
    {
        
        public int TrajetId { get; set; }
        public int CreatorId { get; set; }
        public string googleCo { get; set; }
    }
}
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.Api.Message
{
    public class SendMessageViewModel
    {
        [JsonIgnore]
        public string Me { get; set; }

        [Required]
        public string Receiver { get; set; }

        [Required]
        [StringLength(1000)]
        public string MessageText { get; set; }
    }
}
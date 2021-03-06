﻿using RiderQc.Web.ViewModels.User;
using System;
using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.Api.Message
{
    public class MessageConversationViewModel
    {
        public bool Me { get; set; }
        public int MessageId { get; set; }

        [Required]
        [StringLength(1000)]
        public string MessageText { get; set; }
        
        public DateTime TimeStamp { get; set; }

        public DateTime? Read { get; set; }

        public UserSimpleViewModel Sender { get; set; }

        public UserSimpleViewModel Receiver { get; set; }
    }
}
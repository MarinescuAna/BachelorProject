﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Domain.Models.Domain
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        public int ChatID { get; set; }
        public Chat Chat { get; set; }
        public string Content { get; set; }
        public DateTime? DateSent { get; set; }
        public User User { get; set; }
    }
}

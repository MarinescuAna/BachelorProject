using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.DataAccess.Domain.Chat.Domain
{
    public class MessageView
    {
        public string MessageKey { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public string DateSent { get; set; }

    }
}

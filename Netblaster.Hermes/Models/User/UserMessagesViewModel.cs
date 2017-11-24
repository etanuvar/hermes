using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Netblaster.Hermes.DAL.Model;

namespace Netblaster.Hermes.WebUI.Models.User
{
    public class UserMessagesViewModel
    {
        public IEnumerable<UserMessage> Received { get; set; }

        public IEnumerable<UserMessage> Deleted { get; set; }

        public IEnumerable<UserMessage> Sent { get; set; }

        public bool IsReceived { get; set; }

        public bool IsTrash { get; set; }

        public bool IsSent { get; set; }
    }
}
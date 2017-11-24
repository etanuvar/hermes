using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netblaster.Hermes.WebUI.Models.Management
{
    public class SendMessageViewModel
    {
        public string UserId { get; set; }
        public string Title { get; set; }

        public string Message { get; set; }
    }
}
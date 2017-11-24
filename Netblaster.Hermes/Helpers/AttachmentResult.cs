using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netblaster.Hermes.WebUI.Helpers
{
    public class AttachmentResult
    {
        public AttachmentResult()
        {
            Files = new List<string>();
        }

        public List<string> Files { get; set; }
        public string UniqueId { get; set; }
    }
}
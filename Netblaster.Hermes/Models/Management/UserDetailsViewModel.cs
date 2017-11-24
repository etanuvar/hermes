using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Netblaster.Hermes.DAL.Model;

namespace Netblaster.Hermes.WebUI.Models.Management
{
    public class UserDetailsViewModel
    {

        public ApplicationUser ApplicationUser { get; set; }

        public int ContactCount { get; set; }

        public int TaskCount { get; set; }
    }
}
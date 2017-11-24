using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Netblaster.Hermes.WebUI.Models.Management
{
    public class AddUserToGroupVM
    {

        public int SelectedGroupId
        {
            get;
            set;
        }

        public string SelectedUserId { get; set; }

    }
}
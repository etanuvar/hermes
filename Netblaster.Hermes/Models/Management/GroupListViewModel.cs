using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netblaster.Hermes.WebUI.Models.Management
{
    public class GroupListViewModel
    {

        public IEnumerable<UserGroupViewModel> Groups { get; set; }


        public List<SelectListItem> Users { get; set; }

        public AddUserToGroupVM AddUserVm { get; set; }

        [Display(Name = "Nazwa nowej grupy")]
        public string NewGroupName { get; set; }

    }
}
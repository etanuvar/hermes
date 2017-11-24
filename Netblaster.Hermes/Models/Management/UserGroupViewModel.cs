using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Netblaster.Hermes.DAL.Model;

namespace Netblaster.Hermes.WebUI.Models.Management
{
    public class UserGroupViewModel
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
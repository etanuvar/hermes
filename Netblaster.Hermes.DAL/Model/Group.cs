using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netblaster.Hermes.DAL.Model
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}

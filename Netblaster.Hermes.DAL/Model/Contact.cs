using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netblaster.Hermes.DAL.Model
{
    public class Contact : CalendarComponent
    {
        public int KntId { get; set; }

        public string SelectedContactType { get; set; }

        public ICollection<Attachment> Attachments { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}

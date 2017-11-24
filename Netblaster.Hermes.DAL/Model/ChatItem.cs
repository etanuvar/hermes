using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netblaster.Hermes.DAL.Model
{
    public class ChatItem
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Text { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}

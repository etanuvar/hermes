using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netblaster.Hermes.DAL.Model
{
    public class UserMessage
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Text { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsRead { get; set; }
        
        public string SenderDisplayName { get; set; }
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual ApplicationUser Receiver { get; set; }
    }
}

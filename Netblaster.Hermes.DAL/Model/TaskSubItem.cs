using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netblaster.Hermes.DAL.Model
{
    public class TaskSubItem
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsFinished { get; set; }

        public DateTime? FinishedDate { get; set; }

        public string FinishedById { get; set; }

        public virtual ApplicationUser FinishedBy { get; set; }

        public int TaskItemId { get; set; }

        public virtual TaskItem TaskItem { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netblaster.Hermes.DAL.Model
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public byte[] BinaryData { get; set; }

        public string FileName { get; set; }

        public string MimeType { get; set; }

        public int? TaskItemId { get; set; }

        public virtual TaskItem TaskItem { get; set; }

        public int? ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        public int? TaskDetailId { get; set; }

        public virtual TaskDetail TaskDetail { get; set; }

        public int? TaskSubItemId { get; set; }

        public virtual TaskSubItem TaskSubItem { get; set; }

    }
}

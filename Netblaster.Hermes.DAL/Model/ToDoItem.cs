using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netblaster.Hermes.DAL.Model
{
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Note { get; set; }

        public bool IsReady { get; set; }

        public int? FinishedById { get; set; }
        public ApplicationUser FinishedBy { get; set; }

        public int TaskItemId { get; set; }

        public virtual TaskItem TaskItem { get; set; }
    }
}

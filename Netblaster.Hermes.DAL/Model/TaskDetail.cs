using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netblaster.Hermes.DAL.Model.Enums;

namespace Netblaster.Hermes.DAL.Model
{
    public class TaskDetail
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int TaskItemId { get; set; }

        public string Text { get; set; }

        public TaskDetailType TaskDetailType { get; set; }

        public virtual TaskItem TaskItem { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}

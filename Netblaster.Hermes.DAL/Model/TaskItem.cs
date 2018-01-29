using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Netblaster.Hermes.DAL.Model.Enums;

namespace Netblaster.Hermes.DAL.Model
{
    public class TaskItem : CalendarComponent
    {
        public string Title { get; set; }

        public string CreatedById { get; set; }

        public DateTime DeadlineDate { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        public virtual ICollection<ToDoItem> ToDoItems { get; set; }

        public virtual ICollection<WorkTime> WorkTimes { get; set; }

        public virtual ICollection<TaskDetail> TaskDetails { get; set; }

        public virtual ICollection<TaskItemUser> TaskItemUsers { get; set; }

        public virtual ICollection<TaskSubItem> TaskSubItems { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }

        public int? KntId { get; set; }

        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }

        public string SelectedUserId { get; set; }

        public string FinishedByDisplay { get; set; }

        [ForeignKey("SelectedUserId")]
        public virtual ApplicationUser SelectedUser { get; set; }

        public TaskItemStatus ItemStatus { get; set; }

        [NotMapped]
        public string DisplayId => $"TASK{Id:D5}";
    }
}

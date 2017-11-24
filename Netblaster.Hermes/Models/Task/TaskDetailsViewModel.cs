using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Netblaster.Hermes.DAL.Model;
using Netblaster.Hermes.DAL.Model.Enums;

namespace Netblaster.Hermes.WebUI.Models.Task
{
    public class TaskDetailsViewModel
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Note { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime DeadlineDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<SelectListItem> Users { get; set; }

        [Required]
        [Display(Name = "Przydzielone do")]
        public string SelectedUserId { get; set; }

        public string Title { get; set; }

        public string CreatedById { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        public virtual ICollection<ToDoItem> ToDoItems { get; set; }

        public virtual ICollection<WorkTime> WorkTimes { get; set; }

        public virtual ICollection<TaskDetail> TaskDetails { get; set; }

        public virtual ICollection<TaskItemUser> TaskItemUsers { get; set; }

        public virtual ICollection<TaskSubItem> TaskSubItems { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }

        public TaskItemStatus ItemStatus { get; set; }

        public string DisplayId => $"TASK{Id:D5}";

        [Required]
        [Display(Name="Treść komentarza")]
        public string Comment { get; set; }

    }
}
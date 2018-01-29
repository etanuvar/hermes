using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Netblaster.Hermes.DAL.Model;
using Netblaster.Hermes.DAL.Model.Enums;
using Netblaster.Hermes.DAL.Optima;

namespace Netblaster.Hermes.BLL.DTOs
{
    public class TaskListDto
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Tytuł zadania")]
        public string Title { get; set; }

        public string CreatedById { get; set; }

        [Display(Name = "Zgłaszający")]
        public ApplicationUser CreatedBy { get; set; }

        public int? GroupId { get; set; }

        [Display(Name = "Przydzielone do")]
        public virtual Group Group { get; set; }


        [Display(Name = "Data utworzenia")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Treść zadania")]
        public string Note { get; set; }

        public string ApplicationUserId { get; set; }

        [Display(Name = "Przydzielone do")]
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Data zakończenia")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Termin graniczny")]
        public DateTime DeadlineDate { get; set; }

        [Display(Name = "Status")]
        public TaskItemStatus ItemStatus { get; set; }

        public int KntId { get; set; }

        public Kontrahenci Kontrahent { get; set; }
    }
}

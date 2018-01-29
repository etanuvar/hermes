using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Netblaster.Hermes.BLL.DTOs.FilterBoxes.Base;
using Netblaster.Hermes.DAL.Model;
using Netblaster.Hermes.DAL.Model.Enums;

namespace Netblaster.Hermes.BLL.DTOs.FilterBoxes
{
    public class TaskFilterDto : FilterBoxDTO
    {
        public TaskFilterDto()
        {
            
        }
        public TaskFilterDto(List<SelectListItem> groupList, List<SelectListItem> usersList)
        {
            AvailableGroups = groupList;
            AvailableUsers = usersList;
        }

        public List<SelectListItem> AvailableGroups { get; set; }

        public List<SelectListItem> AvailableUsers { get; set; }

        [Display(Name = "Przydzielone do grupy")]
        public int? SelectedGroupId { get; set; }

        [Display(Name = "Tytuł zadania")]
        public string Title { get; set; }

        [Display(Name = "Zgłaszający")]
        public string CreatedById { get; set; }

        [Display(Name = "Zgłaszający")]
        public string CreatedBy { get; set; }


        [Display(Name = "Data utworzenia od")]
        public DateTime? CreateDateFrom { get; set; }


        [Display(Name = "Data utworzenia do")]
        public DateTime? CreateDateTo { get; set; }

        [Display(Name = "Treść zadania")]
        public string Note { get; set; }

        [Display(Name = "Klient (nazwa lub akronim)")]
        public string ClientName { get; set; }

        [Display(Name = "Przydzielone do użytkownika")]
        public string AffectedTo { get; set; }

        [Display(Name = "Termin graniczny od")]
        public DateTime? DeadlineDateFrom { get; set; }

        [Display(Name = "Termin graniczny do")]
        public DateTime? DeadlineDateTo { get; set; }

        [Display(Name = "Data zakończenia od")]
        public DateTime? EndDateFrom { get; set; }

        [Display(Name = "Data zakończenia do")]
        public DateTime? EndDateTo { get; set; }

        [Display(Name = "Status")]
        public TaskItemStatus? ItemStatus { get; set; }
    }
}

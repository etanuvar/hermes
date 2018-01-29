using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Netblaster.Hermes.DAL.Model;

namespace Netblaster.Hermes.WebUI.Models.Task
{
    public class AddTaskViewModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Tytuł zadania")]
        public string Title { get; set; }

        [Required]
        public string CreatedById { get; set; }

        [Required]
        [Display(Name = "Data utworzenia")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateDate { get; set; }

        [Required]
        [Display(Name = "Godzina utworzenia")]
        public string CreateTime { get; set; }

        [Required]
        [Display(Name = "Godzina zakończenia")]
        
        public string EndTime { get; set; }


        [Required]
        [Display(Name = "Treść zadania")]
        public string Note { get; set; }

        public List<SelectListItem> Groups { get; set; }

        public List<SelectListItem> Users { get; set; }

        [Display(Name = "Kontrahent")]
        public int? KntId { get; set; }

        public string ClientDisplay { get; set; }

        [Display(Name = "Przydzielone do grupy")]
        public int? SelectedGroupId { get; set; }

        [Display(Name = "Przydzielone do użytkownika")]
        public string SelectedUserId { get; set; }

        [Required]
        [Display(Name = "Termin graniczny")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DeadlineDate { get; set; }

        
        [Display(Name = "Zgłaszający")]
        public ApplicationUser CreatedBy { get; set; }


        public string UniqueId { get; set; }

        public string[] subItemElement { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            var properCreateDate = CreateDate.AddHours(int.Parse(CreateTime.Split(':')[0]));
            properCreateDate = properCreateDate.AddMinutes(int.Parse(CreateTime.Split(':')[1]));

            var properEndDate = DeadlineDate.AddHours(int.Parse(EndTime.Split(':')[0]));
            properEndDate = properEndDate.AddMinutes(int.Parse(EndTime.Split(':')[1]));

            var timespan = (properEndDate - properCreateDate).TotalDays;

            if (timespan < 0)
            {
                results.Add(new ValidationResult("Data planowanego zakończenia zadania musi być większa od daty utworzenia", new List<string>() { "EndDate" }));
            }

            if (string.IsNullOrEmpty(SelectedUserId) && SelectedGroupId == null)
            {
                results.Add(new ValidationResult("Musisz przydzielić zadanie do grupy albo do użytkownika", new List<string>() { "SelectedUserId", "SelectedGroupId" }));
            }

            return results;
        }
    }
}
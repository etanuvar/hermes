using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Netblaster.Hermes.DAL.Model;

namespace Netblaster.Hermes.WebUI.Models.Contact
{
    public class AddContactViewModel
    {

        public AddContactViewModel()
        {
            ContactTypes = new List<SelectListItem>();
            Attachments = new List<string>();
        }

        public int Id { get; set; }

        [Display(Name = "Data kontaktu")]
        [Required(ErrorMessage = "Data kontaktu jest wymagana.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Godzina kontaktu")]
        [Required(ErrorMessage = "Godzina kontaktu jest wymagana.")]
        public string CreateTime { get; set; }

        [Display(Name = "Typ kontaktu")]
        [Required(ErrorMessage = "Wybierz typ kontaktu.")]
        public string SelectedContactType { get; set; }

        [Required(ErrorMessage = "Wpisz notatkę z kontaktu.")]
        [Display(Name = "Opis kontaktu")]
        [AllowHtml]
        public string Note { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime? EndDate { get; set; }

        [Display(Name = "Kontrahent")]
        [Range(1, int.MaxValue, ErrorMessage = "Wyszukaj kontrahenta.")]
        public int KntId { get; set; }

        public string ClientDisplay { get; set; }

        public List<SelectListItem> ContactTypes { get; set; }

        public bool IsImported { get; set; }

        public string UniqueId { get; set; }

        [Display(Name="Dodać zadanie z przypomnieniem?")]
        public bool AddTaskAfterContact { get; set; }

        public List<string> Attachments { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Netblaster.Hermes.BLL.DTOs.FilterBoxes.Base;

namespace Netblaster.Hermes.BLL.DTOs.FilterBoxes
{
    public class ContactFilterDto : FilterBoxDTO
    {
        public ContactFilterDto()
        {
            
        }

        public ContactFilterDto(List<SelectListItem> users, List<SelectListItem> contactTypes)
        {
            AvailableUsers = users;
            AvailableContactTypes = contactTypes;
        }

        [Display(Name = "Data kontaktu")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Typ kontaktu")]
        public string SelectedContactType { get; set; }

        [Display(Name = "Opis kontaktu")]
        public string Note { get; set; }

        [Display(Name = "Kontrahent")]
        public string ClientName { get; set; }

        [Display(Name = "Pracownik")]
        public string WorkerName { get; set; }

        [Display(Name = "Zgłaszający")]
        public string CreatedById { get; set; }

        public List<SelectListItem> AvailableUsers { get; set; }

        public List<SelectListItem> AvailableContactTypes { get; set; }
    }
}

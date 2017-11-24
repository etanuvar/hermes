using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netblaster.Hermes.BLL.DTOs.FilterBoxes.Base;

namespace Netblaster.Hermes.BLL.DTOs.FilterBoxes
{
    public class ContactFilterDto : FilterBoxDTO
    {
        [Display(Name = "Data kontaktu")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Typ kontaktu")]
        public string SelectedContactType { get; set; }

        [Display(Name = "Opis kontaktu")]
        public string Note { get; set; }

        [Display(Name = "Nazwa firmy")]
        public string ClientName { get; set; }

        [Display(Name = "Pracownik")]
        public string WorkerName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netblaster.Hermes.DAL.Model;

namespace Netblaster.Hermes.BLL.DTOs
{
    public class ContactListDto
    {
        public int Id { get; set; }

        [Display(Name = "Data kontaktu")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Typ kontaktu")]
        public string SelectedContactType { get; set; }

        [Display(Name = "Opis kontaktu")]
        public string Note { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime? EndDate { get; set; }

        [Display(Name = "Kontrahent")]
        public string ClientName { get; set; }

        [Display(Name = "Pracownik")]
        public string WorkerName { get; set; }

        public int KntId { get; set; }

    }
}

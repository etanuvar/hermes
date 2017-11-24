using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Netblaster.Hermes.BLL.DTOs;
using Netblaster.Hermes.DAL.Model;

namespace Netblaster.Hermes.WebUI.Models.Contact
{
    public class ContactDetailsViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Data kontaktu")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Typ kontaktu")]
        public string SelectedContactType { get; set; }

        [Display(Name = "Opis kontaktu")]
        public string Note { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime? EndDate { get; set; }

        public int KntId { get; set; }

        public ClientListDto ClientDetails { get; set; }


    }
}
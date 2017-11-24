using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Netblaster.Hermes.WebUI.Models.Client
{
    public class ClientSearchViewModel
    {
        public int Knt_KntId { get; set; }

        [Display(Name = "Krótka nazwa")]
        public string Knt_Kod { get; set; }

        [Display(Name = "Nazwa kontrahenta")]
        public string Knt_Nazwa1 { get; set; }

        [Display(Name = "Rozszerzenie nazwy")]
        public string Knt_Nazwa2 { get; set; }

        [Display(Name = "Województwo")]
        public string Knt_Wojewodztwo { get; set; }
        [Display(Name = "Powiat")]
        public string Knt_Powiat { get; set; }
        [Display(Name = "Gmina")]
        public string Knt_Gmina { get; set; }
        [Display(Name = "Ulica")]
        public string Knt_Ulica { get; set; }
        [Display(Name = "Numer domu")]
        public string Knt_NrDomu { get; set; }
        [Display(Name = "Numer lokalu")]
        public string Knt_NrLokalu { get; set; }
        [Display(Name = "Miasto")]
        public string Knt_Miasto { get; set; }
        [Display(Name = "Kod pocztowy")]
        public string Knt_KodPocztowy { get; set; }
        [Display(Name = "NIP")]
        public string Knt_Nip { get; set; }
        [Display(Name = "E-mail")]
        public string Knt_Email { get; set; }

    }
}
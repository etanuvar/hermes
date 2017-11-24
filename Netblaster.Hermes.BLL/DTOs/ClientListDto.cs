using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netblaster.Hermes.BLL.DTOs
{
    public class ClientListDto
    {
        [Display(Name = "ID")]
        public int Knt_KntId { get; set; }

        [Display(Name = "Krótka nazwa")]
        public string Knt_Kod { get; set; }

        [Display(Name = "Nazwa kontrahenta")]
        public string Knt_Nazwa1 { get; set; }

        [Display(Name = "Rozszerzenie nazwy")]
        public string Knt_Nazwa2 { get; set; }

        [Display(Name = "Województwo")]
        public string Knt_Wojewodztwo { get; set; }

        [Display(Name = "Kraj")]
        public string Knt_Kraj { get; set; }
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
        [Display(Name = "Regon")]
        public string Knt_Regon { get; set; }
        [Display(Name = "Email")]
        public string Knt_Email { get; set; }
        [Display(Name = "Strona WWW")]
        public string Knt_URL { get; set; }
        [Display(Name = "Telefon 1")]
        public string Knt_Telefon1 { get; set; }

        [Display(Name = "Telefon 2")]
        public string Knt_Telefon2 { get; set; }

        [Display(Name = "Telefon 3")]
        public string Knt_TelefonSms { get; set; }

        [Display(Name = "Fax")]
        public string Knt_Fax { get; set; }

        [Display(Name = "Typ")]
        public string Knt_Grupa { get; set; }
    }
}

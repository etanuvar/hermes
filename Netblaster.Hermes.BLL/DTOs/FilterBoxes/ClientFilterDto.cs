using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netblaster.Hermes.BLL.DTOs.FilterBoxes.Base;

namespace Netblaster.Hermes.BLL.DTOs.FilterBoxes
{
    public class ClientFilterDto : FilterBoxDTO
    {
        [Display(Name = "Nazwa firmy")]
        public string Knt_Nazwa1 { get; set; }

        [Display(Name = "Adres e-mail")]
        public string Knt_Email { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string Knt_KodPocztowy { get; set; }

        [Display(Name = "Miasto")]
        public string Knt_Powiat { get; set; }

        [Display(Name = "Telefon")]
        public string Knt_Telefon1 { get; set; }

        [Display(Name = "Ulica")]
        public string Knt_Ulica { get; set; }

        [Display(Name = "NIP")]
        public string Knt_Nip { get; set; }

        [Display(Name = "Akronim")]
        public string Knt_Kod { get; set; }

    }
}

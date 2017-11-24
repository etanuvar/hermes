using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netblaster.Hermes.DAL.Model.Enums
{
    public enum TaskItemStatus
    {
        [Display(Name="Oczekuje")]
        Pending = 0,
        [Display(Name = "W trakcie")]
        InProgress = 10,
        [Display(Name = "Odrzucone")]
        Rejected = 20,
        [Display(Name = "Wykonane")]
        Done = 30,
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netblaster.Hermes.DAL.Model
{
    public class CalendarComponent
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Note { get; set; }

        public DateTime? EndDate { get; set; }
    }
}

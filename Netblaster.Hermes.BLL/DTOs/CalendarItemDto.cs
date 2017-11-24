using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netblaster.Hermes.DAL.Model.Enums;

namespace Netblaster.Hermes.BLL.DTOs
{
    public class CalendarItemDto
    {
        public int ID { get; set; }

        public string taskDisplay { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }

        public string startDisplay { get; set; }

        public string statusDisplay { get; set; }

        public DateTime end { get; set; }
        public bool allDay { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }

        public int customCalendarItemId { get; set; }

        public string customAssignedToDisplay { get; set; }
        public string customCreatedByDisplay { get; set; }
       

    }
}

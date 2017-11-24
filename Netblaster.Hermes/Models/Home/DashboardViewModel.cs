using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netblaster.Hermes.WebUI.Models.Home
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            //NotFinishedTasks = new List<DashboardTaskItem>();
        }


        public int AllClientsCount { get; set; }

        public int AllContactWithClients { get; set; }

        public int AllTasksCount { get; set; }

        public int AllTaskToday { get; set; }

        public int AllTaskByYou { get; set; }

        public int AllTaskTodayByYou { get; set; }

        public double TaskPerDayCount { get; set; }

        public int FinishedTaskPercent { get; set; }

        public int AllContactsCount { get; set; }

        public int AllContactsByYou { get; set; }

        public int AllContactsToday { get; set; }

        public int AllContactsTodayByYou { get; set; }
    }

    public class DashboardTaskItem
    {
        public DateTime Date { get; set; }

        public int Count { get; set; }
    }
}
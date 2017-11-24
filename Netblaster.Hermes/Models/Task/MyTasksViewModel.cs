using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Netblaster.Hermes.BLL.DTOs;

namespace Netblaster.Hermes.WebUI.Models.Task
{
    public class MyTasksViewModel
    {
        public int? SelectedId { get; set; }
        public IEnumerable<TaskListDto> Tasks { get; set; }
    }
}
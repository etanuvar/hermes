using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netblaster.Hermes.DAL.Model;

namespace Netblaster.Hermes.DAL
{
    public interface IHermesDataContext
    {

        Task<int> SaveChangesAsync();

        void Dispose();

        DbSet<ApplicationUser> Users { get; set; }
        DbSet<CalendarComponent> CalendarComponents { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<TaskItem> TaskItems { get; set; }
        DbSet<ToDoItem> ToDoItems { get; set; }

        DbSet<WorkTime> WorkTimes { get; set; }
    }
}

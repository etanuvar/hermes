using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Netblaster.Hermes.DAL.Model
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreateDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public byte[] Photo { get; set; }

        public string PhotoImage => $"data:image/png;base64,{Convert.ToBase64String(Photo)}";

        public DateTime LastLoginDate { get; set; }


        public virtual ICollection<WorkTime> WorkTimes { get; set; }

        public virtual ICollection<CalendarComponent> CalendarComponents { get; set; }

        public virtual ICollection<TaskDetail> TaskDetails { get; set; }

        public virtual ICollection<UserMessage> UserMessages { get; set; }


        public virtual ICollection<ChatItem> ChatItems { get; set; }

        public virtual ICollection<TaskItemUser> TaskItemUsers { get; set; }
        
        public virtual ICollection<UserGroup> UserGroups { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        [NotMapped]
        public string DisplayName => $"{FirstName} {LastName}";

        public bool HasClaim(string claim)
        {
            return Claims.Any(x => x.ClaimValue == "Administrator");
        }
    }
}

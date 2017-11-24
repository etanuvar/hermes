using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Netblaster.Hermes.DAL;
using Netblaster.Hermes.DAL.Model;

namespace Netblaster.Hermes.BLL.Manager
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var hermesDbContext = context.Get<HermesDataContext>();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(hermesDbContext));

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = false,
            };

            userManager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 8,
            };

            return userManager;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Netblaster.Hermes.BLL.Manager;
using Netblaster.Hermes.WebUI.Controllers.Base;

namespace Netblaster.Hermes.WebUI.Filters
{
    public class CustomActionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as BaseController;

            if (controller != null && controller.CurrentUser == null && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                controller.CurrentUser = controller.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>()
                    .FindById(filterContext.HttpContext.User.Identity.GetUserId());

                filterContext.HttpContext.Session["ApplicationUser"] = controller.CurrentUser;
            }
        }
    }
}
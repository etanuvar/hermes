using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Netblaster.Hermes.BLL.Manager;
using Netblaster.Hermes.DAL.Model;

namespace Netblaster.Hermes.WebUI.Controllers.Base
{
    public class BaseController : Controller
    {
        public ApplicationUser CurrentUser { get; set; }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            } 
        }

        public ApplicationUserManager UserManager
        {
            get
            {
               return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            } 
        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        } 

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public BaseController()
        {

        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Dashboard", "Home");
        }

        protected void SetHeaders(string pageTitle, string pageDescription, string actionName, string controllerName, string buttonLink = null, string buttonText = null)
        {
            ViewBag.PageTitle = pageTitle;
            ViewBag.Description = pageDescription;
            ViewBag.Action = actionName;
            ViewBag.Controller = controllerName;

            if (buttonLink != null && buttonText != null)
            {
                ViewBag.ButtonText = buttonText;
                ViewBag.ButtonHref = buttonLink;
            }

        }

    }
}
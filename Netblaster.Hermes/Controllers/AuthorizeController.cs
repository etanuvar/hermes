using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Netblaster.Hermes.DAL.Model;
using Netblaster.Hermes.WebUI.Controllers.Base;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Netblaster.Hermes.BLL.Manager;
using Netblaster.Hermes.DAL;
using Netblaster.Hermes.DAL.Model.Enums;
using Netblaster.Hermes.DAL.Optima;
using Netblaster.Hermes.WebUI.Helpers;
using Netblaster.Hermes.WebUI.Models.Authorize;

namespace Netblaster.Hermes.WebUI.Controllers
{
    [Authorize]
    public class AuthorizeController : BaseController
    {
        private readonly Entities _optimaDbContext;
        private readonly HermesDataContext _hermesDataContext;

        public AuthorizeController()
        {
            _optimaDbContext = new Entities();
            _hermesDataContext = new HermesDataContext();
        }


        [AllowAnonymous]
        public ActionResult Register()
        {
            var registerViewModel = new RegisterViewModel
            {
                CreateDate = DateTime.Now,
                LastLoginDate = DateTime.Now
            };

            return View(registerViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Exclude = "Photo")]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<ApplicationUser>(model);
                if (string.IsNullOrEmpty(user.JobTitle))
                {
                    user.JobTitle = ConfigurationManager.AppSettings["DefaultJobTitle"];
                }
                //Handle profile photo for user
                byte[] imageData = null;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["Photo"];
                    if (poImgFile.ContentLength > 0)
                    {
                        using (var binary = new BinaryReader(poImgFile.InputStream))
                        {
                            imageData = binary.ReadBytes(poImgFile.ContentLength);
                        }
                        user.Photo = imageData;
                    }
                    else
                    {
                        user.Photo = Convert.FromBase64String(ConfigurationManager.AppSettings["DefaultUserPhoto"]);
                    }
                }
                user.EmailConfirmed = false;

                var templateFile = Server.MapPath(Url.Content("~/Content/template.txt"));
                var msg = EmailHelper.GenerateNewUserMessage($"http://{Request.Url.Authority}", templateFile, CurrentUser, user);
                var users = _hermesDataContext.Users.Where(x => x.Claims.Any(y => y.ClaimValue == "Administrator"));
                EmailHelper.SendEmailToUsers(msg, users);


                user.Claims.Add(new IdentityUserClaim {ClaimType = "Role", ClaimValue = UserRoleEnum.StandardUser.ToString(), UserId = user.Id});

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Login", "Authorize");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Nie znaleziono użytkownika");
            }
            else
            {
                if (user.EmailConfirmed == false)
                {
                    ModelState.AddModelError("", "Twoje konto nie jest obecnie aktywne. Skontaktuj się z administratorem w celu aktywowania konta.");
                    return View(model);
                }
                var result = await SignInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        var userx = _hermesDataContext.Users.Find(user.Id);
                        userx.LastLoginDate = DateTime.Now;
                        _hermesDataContext.SaveChanges();
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Błędny użytkownik lub hasło.");
                        break;
                }
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true


            return View(model);
        }


        [HttpGet]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


    }
}
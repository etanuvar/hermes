using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Mvc;
using Netblaster.Hermes.DAL;
using Netblaster.Hermes.DAL.Model;
using Netblaster.Hermes.DAL.Optima;
using Netblaster.Hermes.WebUI.Controllers.Base;
using Netblaster.Hermes.WebUI.Filters;
using Netblaster.Hermes.WebUI.Models.User;

namespace Netblaster.Hermes.WebUI.Controllers
{
    [Authorize]
    [CustomActionFilter]
    public class UserController : BaseController
    {
        private readonly Entities _optimaDbContext;
        private readonly HermesDataContext _hermesDataContext;

        public UserController()
        {
            _optimaDbContext = new Entities();
            _hermesDataContext = new HermesDataContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(CurrentUser);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var appUser = _hermesDataContext.Users.Find(id);

            return View(appUser);
        }

        [HttpPost]
        public ActionResult Index([Bind(Exclude = "Photo")]ApplicationUser user)
        {
            var appUser = _hermesDataContext.Users.Find(user.Id);

            appUser.PhoneNumber = user.PhoneNumber;
            appUser.JobTitle = user.JobTitle;
            appUser.Email = user.Email;
            appUser.FirstName = user.FirstName;
            appUser.LastName = user.LastName;
            appUser.Email = user.Email;

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase poImgFile = Request.Files["Photo"];
                if (poImgFile.ContentLength > 0)
                {
                    byte[] imageData = null;
                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                    appUser.Photo = imageData;
                }
            }

            _hermesDataContext.SaveChanges();
            return RedirectToAction("Dashboard", "Home");
        }

        public ActionResult Messages()
        {
            var received = CurrentUser.UserMessages.Where(x => x.ReceiverId == CurrentUser.Id && !x.IsDeleted);
            var deleted = CurrentUser.UserMessages.Where(x => x.IsDeleted);
            var sent = CurrentUser.UserMessages.Where(x => x.SenderId == CurrentUser.Id && !x.IsDeleted);

           ViewBag.Users = _hermesDataContext.Users.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.Id.ToString()
            }).ToList();

            var viewModel = new UserMessagesViewModel()
            {
                Deleted = deleted,
                Sent = sent,
                Received = received
            };

            return View(viewModel);
        }

        public ActionResult SoftDeleteMsg(int id)
        {
            var userMessage = _hermesDataContext.UserMessages.Find(id);
            if (userMessage != null)
            {
                userMessage.IsDeleted = true;
                _hermesDataContext.SaveChanges();
            }

            return RedirectToAction("Messages");
        }

        public ActionResult HardDeleteMsg(int id)
        {
            var userMessage = _hermesDataContext.UserMessages.Find(id);
            if (userMessage != null)
            {
                _hermesDataContext.UserMessages.Remove(userMessage);
                _hermesDataContext.SaveChanges();
            }

            return RedirectToAction("Messages");
        }

        public ActionResult MessageDetails(int id)
        {
            var message = _hermesDataContext.UserMessages.Find(id);
            if (CurrentUser.Id == message.ReceiverId)
            {
                message.IsRead = true;
                _hermesDataContext.SaveChanges();
            }
            return View(message);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Netblaster.Hermes.DAL;
using Netblaster.Hermes.DAL.Model;
using Netblaster.Hermes.DAL.Optima;
using Netblaster.Hermes.WebUI.Controllers.Base;
using Netblaster.Hermes.WebUI.Filters;
using Netblaster.Hermes.WebUI.Helpers;
using Netblaster.Hermes.WebUI.Models.Contact;
using Netblaster.Hermes.WebUI.Models.Management;
using Group = Netblaster.Hermes.DAL.Model.Group;

namespace Netblaster.Hermes.WebUI.Controllers
{

    [Authorize]
    [CustomActionFilter]
    public class ManagementController : BaseController
    {
        private readonly Entities _optimaDbContext;
        private readonly HermesDataContext _hermesDataContext;

        public ManagementController()
        {
            _optimaDbContext = new Entities();
            _hermesDataContext = new HermesDataContext();
        }

        [HttpPost]
        public ActionResult SendMessage(SendMessageViewModel model)
        {
            var newMessage = new UserMessage()
            {
                CreateDate = DateTime.Now,
                IsDeleted = false,
                IsRead = false,
                ReceiverId = model.UserId,
                Text = model.Message,
                SenderId = CurrentUser.Id,
                SenderDisplayName = CurrentUser.DisplayName
            };

            _hermesDataContext.UserMessages.Add(newMessage);
            _hermesDataContext.SaveChanges();
            return RedirectToAction("Messages", "User");
        }

        // GET: Management
        public ActionResult Users()
        {
            ViewBag.CurrentUser = CurrentUser;
            var usersVm = new List<UserDetailsViewModel>();
            var users = _hermesDataContext.Users.Where(x => x.LockoutEnabled == false).AsEnumerable();

            foreach (var user in users)
            {
                var contactCount = _hermesDataContext.Contacts.Count(x => x.ApplicationUserId == user.Id);
                var taskCount = _hermesDataContext.TaskItems.Count(x => x.CreatedById == user.Id);

                usersVm.Add(new UserDetailsViewModel()
                {
                    ApplicationUser = user,
                    ContactCount = contactCount,
                    TaskCount = taskCount
                });
            }

            return View(usersVm);
        }

        public ActionResult DeleteUser(int id)
        {
            var userGroup = _hermesDataContext.UserGroups.Find(id);

            if (userGroup != null)
            {
                _hermesDataContext.UserGroups.Remove(userGroup);
                _hermesDataContext.SaveChanges();
            }
            return RedirectToAction("Groups");
        }

        public ActionResult Groups()
        {
            var userGroups = _hermesDataContext.Groups.Where(x => x.IsActive);
            var groups = Mapper.Instance.Map<IEnumerable<UserGroupViewModel>>(userGroups);

            var usrs = _hermesDataContext.Users.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.Id
            }).ToList();

            var viewModel = new GroupListViewModel()
            {
                Groups = groups,
                Users = usrs,
            };
            foreach (var group in viewModel.Groups)
            {
                foreach (var usGroup in group.UserGroups)
                {
                    usGroup.ApplicationUser = _hermesDataContext.Users.Find(usGroup.ApplicationUserId);
                }
            }

            ViewBag.CurrentUser = CurrentUser;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddUserForGroup(AddUserToGroupVM model)
        {
            var operationResult = new OperationResult();
            var existingRelation =
                _hermesDataContext.UserGroups.Any(x => x.GroupId == model.SelectedGroupId &&
                                                       x.ApplicationUserId == model.SelectedUserId);

            if (existingRelation)
            {
                operationResult = new OperationResult()
                {
                    Success = false,
                    Message = "Użytkownik został już dodany do tej grupy"
                };
                return Json(operationResult, JsonRequestBehavior.AllowGet);
            }

            var newUserGroup = new UserGroup()
            {
                ApplicationUserId = model.SelectedUserId,
                GroupId = model.SelectedGroupId
            };

            _hermesDataContext.UserGroups.Add(newUserGroup);
            _hermesDataContext.SaveChanges();

            var userGroups = _hermesDataContext.Groups.Where(x => x.IsActive).AsEnumerable();
            var groups = Mapper.Instance.Map<IEnumerable<UserGroupViewModel>>(userGroups);
            foreach (var group in groups)
            {
                foreach (var usGroup in group.UserGroups)
                {
                    usGroup.ApplicationUser = _hermesDataContext.Users.Find(usGroup.ApplicationUserId);
                }
            }

            ViewBag.CurrentUser = CurrentUser;
            operationResult.Content = this.RenderRazorViewToString("Partial/_UsersInGroupPartial", groups);
            operationResult.Success = true;

            return Json(operationResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ActivateUser(string id)
        {
            var user = _hermesDataContext.Users.Find(id);
            if (user != null)
            {
                user.EmailConfirmed = true;
                _hermesDataContext.SaveChanges();
            }

            return RedirectToAction("Users");
        }

        public ActionResult AddGroup(string NewGroupName)
        {
            var operationResult = new OperationResult();
            if (ModelState.IsValid)
            {
                try
                {
                    var newGroup = new Group()
                    {
                        CreateDate = DateTime.Now,
                        Name = NewGroupName,
                        IsActive = true
                    };

                    _hermesDataContext.Groups.Add(newGroup);
                    _hermesDataContext.SaveChanges();

                    var userGroups = _hermesDataContext.Groups.Where(x => x.IsActive).AsEnumerable();
                    var groups = Mapper.Instance.Map<IEnumerable<UserGroupViewModel>>(userGroups);


                    ViewBag.CurrentUser = CurrentUser;
                    operationResult.Success = true;
                    operationResult.Content = this.RenderRazorViewToString("Partial/_UsersInGroupPartial", groups);
                }
                catch (Exception e)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Nieznany błąd";
                }
            }
            return Json(operationResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteGroup(int id)
        {
            var group = _hermesDataContext.Groups.Find(id);
            group.IsActive = false;
            _hermesDataContext.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);

        }
    }
}
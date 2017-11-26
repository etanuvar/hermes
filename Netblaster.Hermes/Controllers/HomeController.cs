using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Netblaster.Hermes.DAL;
using Netblaster.Hermes.DAL.Model.Enums;
using Netblaster.Hermes.DAL.Optima;
using Netblaster.Hermes.WebUI.Controllers.Base;
using Netblaster.Hermes.WebUI.Filters;
using Netblaster.Hermes.WebUI.Models.Home;

namespace Netblaster.Hermes.WebUI.Controllers
{
    [Authorize]
    [CustomActionFilter]
    public class HomeController : BaseController
    {
        private readonly Entities _optimaDbContext;
        private readonly HermesDataContext _hermesDataContext;

        public HomeController()
        {
            _optimaDbContext = new Entities();
            _hermesDataContext = new HermesDataContext();
        }

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authorize");
            }

            var viewModel = new DashboardViewModel();
            var finishedTaskDates = string.Empty;
            var finishedTaskValues = string.Empty;
            var startedTaskValues = string.Empty;
            var contactsValues = string.Empty;

            var baseTaskQuery = _hermesDataContext.TaskItems.AsQueryable();
            var baseContactQuery = _hermesDataContext.Contacts.AsQueryable();

            for (int i = 0; i <= 7; i++)
            {
                var baseDate = DateTime.Now.AddDays(-7 + i);
                finishedTaskDates += $"\"{baseDate:dddd}\",";
                finishedTaskValues += $"\"{baseTaskQuery.Count(x => x.EndDate.HasValue && x.EndDate.Value.Year == baseDate.Year && x.EndDate.Value.Month == baseDate.Month && x.EndDate.Value.Day == baseDate.Day)}\",";
                startedTaskValues += $"\"{baseTaskQuery.Count(x => x.CreateDate.Year == baseDate.Year && x.CreateDate.Month == baseDate.Month && x.CreateDate.Day == baseDate.Day)}\",";
                contactsValues += $"\"{baseContactQuery.Count(x => x.CreateDate.Year == baseDate.Year && x.CreateDate.Month == baseDate.Month && x.CreateDate.Day == baseDate.Day)}\",";
            }

            ViewBag.FinishedTasksDates = finishedTaskDates.Substring(0, finishedTaskDates.Length -1);
            ViewBag.FinishedTaskValues = finishedTaskValues.Substring(0, finishedTaskValues.Length - 1);
            ViewBag.StartedTaskValues = startedTaskValues.Substring(0, startedTaskValues.Length - 1);
            ViewBag.ContactsValues = contactsValues.Substring(0, contactsValues.Length - 1);

            viewModel.AllClientsCount = _optimaDbContext.Kontrahenci.Count();

            viewModel.AllContactsCount = baseContactQuery.Count();
            viewModel.AllContactsToday = baseContactQuery.Count(x => x.CreateDate.Year == DateTime.Now.Year && x.CreateDate.Month == DateTime.Now.Month && x.CreateDate.Day == DateTime.Now.Day);
            viewModel.AllContactsByYou = baseContactQuery.Count(x => x.ApplicationUserId == CurrentUser.Id);
            viewModel.AllContactsTodayByYou = baseContactQuery.Count(x => x.CreateDate.Year == DateTime.Now.Year && x.CreateDate.Month == DateTime.Now.Month && x.CreateDate.Day == DateTime.Now.Day && x.ApplicationUserId == CurrentUser.Id);

            viewModel.AllTasksCount = baseTaskQuery.Count();
            viewModel.AllTaskToday = baseTaskQuery.Count(x => x.CreateDate.Year == DateTime.Now.Year && x.CreateDate.Month == DateTime.Now.Month && x.CreateDate.Day == DateTime.Now.Day);
            viewModel.AllTaskByYou = baseTaskQuery.Count(x => x.CreatedById == CurrentUser.Id);
            viewModel.AllTaskTodayByYou = baseTaskQuery.Count(x => x.CreateDate.Year == DateTime.Now.Year && x.CreateDate.Month == DateTime.Now.Month && x.CreateDate.Day == DateTime.Now.Day && x.CreatedById == CurrentUser.Id);

            var finishedTasks = _hermesDataContext.TaskItems.Count(x => x.ItemStatus == TaskItemStatus.Done);
            if (finishedTasks > 0)
            {
                viewModel.FinishedTaskPercent = (finishedTasks / viewModel.AllTasksCount) * 100;
            }

            if (_hermesDataContext.TaskItems.Any())
            {
                var firstTaskDay = _hermesDataContext.TaskItems.Min(x => x.CreateDate);
                var today = DateTime.Now;

                TimeSpan difference = today - firstTaskDay;

                if (difference.TotalDays > 0)
                {
                    viewModel.TaskPerDayCount = _hermesDataContext.TaskItems.Count() / difference.TotalDays;
                }

            }

            return View(viewModel);
        }


        //public ActionResult Authorized()
        //{
        //    return View();
        //}

        //public ActionResult Error(string msg)
        //{
        //    return View(msg);
        //}
    }
}
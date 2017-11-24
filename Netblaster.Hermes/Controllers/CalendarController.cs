using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Netblaster.Hermes.BLL.DTOs;
using Netblaster.Hermes.DAL;
using Netblaster.Hermes.DAL.Extensions;
using Netblaster.Hermes.DAL.Model.Enums;
using Netblaster.Hermes.DAL.Optima;
using Netblaster.Hermes.WebUI.Controllers.Base;
using Netblaster.Hermes.WebUI.Filters;

namespace Netblaster.Hermes.WebUI.Controllers
{
    [Authorize]
    [CustomActionFilter]
    public class CalendarController : BaseController
    {
        private readonly Entities _optimaDbContext;
        private readonly HermesDataContext _hermesDataContext;
        private List<SelectListItem> _groups;

        public CalendarController()
        {
            _optimaDbContext = new Entities();
            _hermesDataContext = new HermesDataContext();
            _groups = new List<SelectListItem>();
        }

        // GET: Calendar
        public ActionResult Index()
        {
            ViewBag.AvailableGroups = GetGroups();
            return View();
        }

        public ActionResult GetCalendarItems(int? id = null)
        {
            ViewBag.AvailableGroups = GetGroups();

            var baseQuery = _hermesDataContext.TaskItems.AsQueryable();
            if (id.HasValue)
            {
                baseQuery = baseQuery.Where(x => x.GroupId == id.Value);
            }

            var items = baseQuery.ToList().Select(x => new CalendarItemDto()
            {
                ID = x.Id,
                allDay = false,
                start = x.CreateDate,
                startDisplay = x.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                end = x.DeadlineDate,
                backgroundColor = x.ItemStatus == TaskItemStatus.Rejected ? "#ea6557" : x.ItemStatus == TaskItemStatus.InProgress ? "#FF6200" : x.ItemStatus == TaskItemStatus.Done ? "#74d348" : "#ccc",
                borderColor = x.ItemStatus == TaskItemStatus.Rejected ? "#ea6557" : x.ItemStatus == TaskItemStatus.InProgress ? "#FF6200" : x.ItemStatus == TaskItemStatus.Done ? "#74d348" : "#ccc",
                title = x.Title,
                customCalendarItemId = x.Id,
                customAssignedToDisplay = x.Group.Name,
                customCreatedByDisplay = x.CreatedBy.FirstName + " " + x.CreatedBy.LastName,
                taskDisplay = x.DisplayId,
                statusDisplay = x.ItemStatus.GetDisplayName(),
            });

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        internal List<SelectListItem> GetGroups()
        {
            if (_groups.Count == 0)
            {
                if (CurrentUser.HasClaim("Administrator"))
                {
                    _groups = _hermesDataContext.Groups.Where(x => x.IsActive).Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                }
                else
                {
                    var groups = CurrentUser.UserGroups.Where(x => x.Group.IsActive);
                    _groups = groups.Select(x => new SelectListItem
                    {
                        Text = x.Group.Name,
                        Value = x.GroupId.ToString()
                    }).ToList();
                }
            }
            return _groups;
        }
    }

}
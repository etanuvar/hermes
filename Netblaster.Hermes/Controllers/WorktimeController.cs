using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Netblaster.Hermes.WebUI.Controllers.Base;
using Netblaster.Hermes.WebUI.Filters;

namespace Netblaster.Hermes.WebUI.Controllers
{
    [Authorize]
    [CustomActionFilter]
    public class WorktimeController : BaseController
    {
        // GET: Worktime
        public ActionResult Index()
        {
            return View();
        }
    }
}
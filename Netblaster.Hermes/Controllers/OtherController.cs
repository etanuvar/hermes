using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper;
using Netblaster.Hermes.BLL.DataFormat;
using Netblaster.Hermes.BLL.DTOs;
using Netblaster.Hermes.BLL.DTOs.FilterBoxes;
using Netblaster.Hermes.DAL;
using Netblaster.Hermes.DAL.Model;
using Netblaster.Hermes.DAL.Optima;
using Netblaster.Hermes.WebUI.Controllers.Base;
using Netblaster.Hermes.WebUI.Filters;
using Netblaster.Hermes.WebUI.Helpers;
using Netblaster.Hermes.WebUI.Models.Authorize;
using Netblaster.Hermes.WebUI.Models.Client;
using PagedList;

namespace Netblaster.Hermes.WebUI.Controllers
{
    [Authorize]
    [CustomActionFilter]
    public class OtherController : BaseController
    {
        private readonly int _defaultPage = 1;
        private readonly int _defaultPageSize = 25;
        private readonly Entities _optimaDbContext;
        private readonly HermesDataContext _hermesDataContext;

        public OtherController()
        {
            _optimaDbContext = new Entities();
            _hermesDataContext = new HermesDataContext();
        }


        [HttpGet]
        public ActionResult Index()
        {
            SetHeaders("Kontrahenci", "Lista wszystkich kontrahentów", "Index", "Client");

            var clients = _optimaDbContext.Kontrahenci.AsEnumerable();
            var clientsDto = Mapper.Instance.Map<IEnumerable<ClientListDto>>(clients);

            SetViewParams("Knt_Nazwa1", _defaultPage, _defaultPageSize);
            var listData = clientsDto.ToPagedList(_defaultPage, _defaultPageSize);

            var viewModel = new ClientListViewModel()
            {
                ListData = listData,
            };


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ClientFilterDto filterBox)
        {
            SetHeaders("Kontrahenci", "Lista wszystkich kontrahentów", "Index", "Client");

            var clients = _optimaDbContext.Kontrahenci.AsEnumerable();
            var clientsDto = Mapper.Instance.Map<IEnumerable<ClientListDto>>(clients);

            var dataFormatter = new ClientDataFormat(filterBox);
            SetViewParams(dataFormatter.SortParam, dataFormatter.Page, dataFormatter.PageSize);

            if (clients == null)
            {
                return PartialView("_PagedListPartial",
                    new List<ClientListDto>().ToPagedList(dataFormatter.Page, dataFormatter.PageSize));
            }

            ViewBag.PageSize = dataFormatter.PageSize;
            var listData = dataFormatter.GetFormattedData(clientsDto)
                .ToPagedList(dataFormatter.Page, dataFormatter.PageSize);

            return PartialView("_PagedListPartial", listData);
        }

        public async Task<ActionResult> Details(int id)
        {
            var client = await _optimaDbContext.Kontrahenci.FindAsync(id);
            var viewModel = Mapper.Instance.Map<ClientListDto>(client);

            return View(viewModel);
        }


        private void SetViewParams(string sortOrder, int page, int pageSize)
        {
            ViewBag.SortParam = sortOrder;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

        }

        [HttpPost]
        public ActionResult AddAttachments(IEnumerable<HttpPostedFileBase> files, string uniqueId)
        {
            var attachmentResult = new AttachmentResult();
            var uploadFileDirectory = WebConfigurationManager.AppSettings["uploadDirectory"];

            var baseDir = Path.Combine(uploadFileDirectory, uniqueId);
            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }

            foreach (var file in files)
            {
                string filePath = Path.Combine(baseDir, file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.File.WriteAllBytes(filePath, ReadData(file.InputStream));
                }
            }

            foreach (var file in Directory.GetFiles(baseDir))
            {
                attachmentResult.Files.Add(Path.GetFileName(file));
            }

            attachmentResult.UniqueId = uniqueId;
            return Json(attachmentResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DownloadFile(int id)
        {
            var attachment = await _hermesDataContext.Attachments.FindAsync(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            return File(attachment.BinaryData, attachment.MimeType);
        }

        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
    }
}
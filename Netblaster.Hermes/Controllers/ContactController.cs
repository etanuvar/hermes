using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper;
using Netblaster.Hermes.BLL.DataFormat;
using Netblaster.Hermes.BLL.DTOs;
using Netblaster.Hermes.BLL.DTOs.FilterBoxes;
using Netblaster.Hermes.BLL.Services;
using Netblaster.Hermes.DAL;
using Netblaster.Hermes.DAL.Model;
using Netblaster.Hermes.DAL.Model.Enums;
using Netblaster.Hermes.DAL.Optima;
using Netblaster.Hermes.WebUI.Controllers.Base;
using Netblaster.Hermes.WebUI.Filters;
using Netblaster.Hermes.WebUI.Helpers;
using Netblaster.Hermes.WebUI.Models.Client;
using Netblaster.Hermes.WebUI.Models.Contact;
using PagedList;

namespace Netblaster.Hermes.WebUI.Controllers
{
    [Authorize]
    [CustomActionFilter]
    public class ContactController : BaseController
    {
        private readonly int _defaultPage = 1;
        private readonly int _defaultPageSize = 25;
        private readonly Entities _optimaDbContext;
        private readonly HermesDataContext _hermesDataContext;
        private List<SelectListItem> _users;

        public ContactController()
        {
            _optimaDbContext = new Entities();
            _hermesDataContext = new HermesDataContext();
            _users = new List<SelectListItem>();
        }

        public ActionResult Index()
        {
            var contactListDto = new List<ContactListDto>();
            var contactClients = _hermesDataContext.Contacts.Select(x => x.KntId).Distinct().ToList();
            var clients = _optimaDbContext.Kontrahenci.Where(x => contactClients.Contains(x.Knt_KntId)).ToList();

            foreach (var item in _hermesDataContext.Contacts.ToList())
            {
                var user = _hermesDataContext.Users.Find(item.ApplicationUserId);
                var client = clients.SingleOrDefault(y => y.Knt_KntId == item.KntId);
                contactListDto.Add(new ContactListDto
                {
                    Id = item.Id,
                    ClientName = client != null ? client.Knt_Nazwa1 : string.Empty,
                    ApplicationUser = item.ApplicationUser,
                    ApplicationUserId = item.ApplicationUserId,
                    CreateDate = item.CreateDate,
                    EndDate = item.EndDate,
                    SelectedContactType = item.SelectedContactType,
                    KntId = item.KntId,
                    Note = item.Note,
                    WorkerName = user.FirstName + " " + user.LastName,
                });
            }

            SetHeaders("Kontakty", "Lista wszystkich kontaktów", "Index", "Contact");
            SetViewParams("CreateDate", _defaultPage, _defaultPageSize);
            var listData = contactListDto.ToPagedList(_defaultPage, _defaultPageSize);

            var contactTypes = _hermesDataContext.Parameters.SingleOrDefault(x => x.Name == "ContactType");
            var splittedTypes = contactTypes.Value.Split(',');
            var typesOfContacts = splittedTypes.Select(x => new SelectListItem
            {
                Text = x,
                Value = x
            }).ToList();

            var viewModel = new ContactListViewModel()
            {
                ListData = listData,
                FilterBox = new ContactFilterDto(GetUses(), typesOfContacts)
            };

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Index(ContactFilterDto filterBox)
        {
            var contactListDto = new List<ContactListDto>();
            var contactClients = _hermesDataContext.Contacts.Select(x => x.KntId).Distinct().ToList();
            var clients = _optimaDbContext.Kontrahenci.Where(x => contactClients.Contains(x.Knt_KntId)).ToList();

            foreach (var item in _hermesDataContext.Contacts.ToList())
            {
                var user = _hermesDataContext.Users.Find(item.ApplicationUserId);
                var client = clients.SingleOrDefault(y => y.Knt_KntId == item.KntId);
                contactListDto.Add(new ContactListDto
                {
                    Id = item.Id,
                    ClientName = client != null ? client.Knt_Nazwa1 : string.Empty,
                    ApplicationUser = item.ApplicationUser,
                    ApplicationUserId = item.ApplicationUserId,
                    CreateDate = item.CreateDate,
                    EndDate = item.EndDate,
                    SelectedContactType = item.SelectedContactType,
                    KntId = item.KntId,
                    Note = item.Note,
                    WorkerName = user.FirstName + " " + user.LastName,
                });
            }

            var dataFormatter = new ContactDataFormat(filterBox);

            SetHeaders("Kontakty", "Lista wszystkich kontaktów", "Index", "Contact");
            SetViewParams(dataFormatter.SortParam, dataFormatter.Page, dataFormatter.PageSize);

            var listData = dataFormatter.GetFormattedData(contactListDto)
                .ToPagedList(dataFormatter.Page, dataFormatter.PageSize);

            return PartialView("_PagedListPartial", listData);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var contactTypes = _hermesDataContext.Parameters.SingleOrDefault(x => x.Name == "ContactType");
            if (contactTypes != null)
            {
                var splittedTypes = contactTypes.Value.Split(',');
                var typesOfContacts = splittedTypes.Select(x => new SelectListItem
                {
                    Text = x,
                    Value = x
                }).ToList();

                var viewModel = new AddContactViewModel
                {
                    ContactTypes = typesOfContacts,
                    CreateDate = DateTime.Now.Date,
                    ApplicationUser = CurrentUser,
                    ApplicationUserId = CurrentUser.Id,
                    UniqueId = Guid.NewGuid().ToString()
                };

                return View(viewModel);
            }

            return RedirectToAction("Error", "Home", new { msg = "Nie zdefiniowano typów kontaktów."});
        }

        public async Task<ActionResult> Details(int id)
        {
            var contact = await _hermesDataContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                var contactDetailsVM = Mapper.Instance.Map<ContactDetailsViewModel>(contact);
                var client = await _optimaDbContext.Kontrahenci.FindAsync(contactDetailsVM.KntId);
                if (client != null)
                {
                    contactDetailsVM.ClientDetails = Mapper.Instance.Map<ClientListDto>(client);
                }

                return View(contactDetailsVM);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(AddContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domainModel = Mapper.Instance.Map<Contact>(model);

                    domainModel.CreateDate = domainModel.CreateDate.AddHours(int.Parse(model.CreateTime.Split(':')[0]));
                    domainModel.CreateDate = domainModel.CreateDate.AddMinutes(int.Parse(model.CreateTime.Split(':')[1]));

                    _hermesDataContext.Contacts.Add(domainModel);
                    _hermesDataContext.SaveChanges();

                    if (!string.IsNullOrEmpty(model.UniqueId))
                    {
                        var uploadFileDirectory = WebConfigurationManager.AppSettings["uploadDirectory"];
                        var baseDir = Path.Combine(uploadFileDirectory, model.UniqueId);

                        if (Directory.Exists(baseDir))
                        {
                            foreach (var file in Directory.GetFiles(baseDir))
                            {
                                var binary = System.IO.File.ReadAllBytes(file);
                                var mime = MimeMapping.GetMimeMapping(file);

                                var newAttachment = new Attachment()
                                {
                                    BinaryData = binary,
                                    ContactId = domainModel.Id,
                                    CreateDate = DateTime.Now,
                                    MimeType = mime,
                                    FileName = Path.GetFileName(file)
                                };

                                _hermesDataContext.Attachments.Add(newAttachment);
                                System.IO.File.Delete(file);
                            }
                            _hermesDataContext.SaveChanges();
                        }
                    }

                    if (model.AddTaskAfterContact)
                    {
                        var client = _optimaDbContext.Kontrahenci.Find(domainModel.KntId);
                        var newTask = new TaskItem()
                        {
                            ItemStatus = TaskItemStatus.InProgress,
                            GroupId = CurrentUser.UserGroups.FirstOrDefault(x => x.Group.IsActive)?.GroupId,
                            CreateDate = domainModel.CreateDate,
                            Title = $"Przypomnienie odnośnie kontaktu z {client.Knt_Nazwa1}",
                            Note = domainModel.Note,
                            CreatedById = CurrentUser.Id,
                            DeadlineDate = domainModel.CreateDate.AddDays(7)
                        };

                        _hermesDataContext.TaskItems.Add(newTask);
                        _hermesDataContext.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Note", e.Message);
                }
            }

            if (!string.IsNullOrEmpty(model.UniqueId))
            {
                model.Attachments = GetExistingAttachments(model.UniqueId);
            }

            var contactTypes = _hermesDataContext.Parameters.SingleOrDefault(x => x.Name == "ContactType");
            var splittedTypes = contactTypes.Value.Split(',');
            var typesOfContacts = splittedTypes.Select(x => new SelectListItem
            {
                Text = x,
                Value = x
            }).ToList();

            model.ContactTypes = typesOfContacts;
            model.ApplicationUser = CurrentUser;
            model.ApplicationUserId = CurrentUser.Id;
            return View(model);
        }

        public List<string> GetExistingAttachments(string guid)
        {
            var attachments = new List<string>();
            var uploadFileDirectory = WebConfigurationManager.AppSettings["uploadDirectory"];
            var baseDir = Path.Combine(uploadFileDirectory, guid);

            if (System.IO.Directory.Exists(baseDir))
            {
                foreach (var file in Directory.GetFiles(baseDir))
                {

                    attachments.Add(Path.GetFileName(file));
                }
            }

            return attachments;
        }

        public ActionResult ConvertMsgToContact(IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                var emailMessage = new MsgReader.Outlook.Storage.Message(files.First().InputStream);
                var clientEmail = emailMessage.Recipients.FirstOrDefault().Email;
                var clientFromDb =
                    _optimaDbContext.Kontrahenci.FirstOrDefault(x => x.Knt_Email.StartsWith(clientEmail));

                if (!emailMessage.SentOn.HasValue)
                {
                    return Json($"E-mail nie został wysłany do odbiorcy.");
                }

                var guid = Guid.NewGuid().ToString();

                if (emailMessage.Attachments != null && emailMessage.Attachments.Any())
                {
                    var uploadFileDirectory = WebConfigurationManager.AppSettings["uploadDirectory"];
                    var baseDir = Path.Combine(uploadFileDirectory, guid);
                    if (!Directory.Exists(baseDir))
                    {
                        Directory.CreateDirectory(baseDir);
                    }

                    foreach (MsgReader.Outlook.Storage.Attachment file in emailMessage.Attachments)
                    {
                        string filePath = Path.Combine(baseDir, file.FileName);
                        if (!System.IO.File.Exists(filePath))
                        {
                            System.IO.File.WriteAllBytes(filePath, file.Data);
                        }
                    }
                }

                var newContactFromEmail = new AddContactViewModel()
                {
                    ApplicationUser = CurrentUser,
                    ApplicationUserId = CurrentUser.Id,
                    CreateDate = emailMessage.SentOn.Value,
                    CreateTime = emailMessage.SentOn.Value.ToShortTimeString(),
                    SelectedContactType = "Email",
                    Note = emailMessage.BodyText,
                    ClientDisplay = clientFromDb?.Knt_Nazwa1,
                    KntId = clientFromDb?.Knt_KntId ?? default(int),
                    IsImported = true,
                    UniqueId = guid
                };

                newContactFromEmail.Attachments = GetExistingAttachments(newContactFromEmail.UniqueId);

                var result = new ImportEmailResult
                {
                    FormData = this.RenderRazorViewToString("Partial/_AddContactFormData", newContactFromEmail),
                    Attachments =
                        this.RenderRazorViewToString("Partial/_AttachmentListPartial", newContactFromEmail.Attachments)
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Błąd podczas konwertowania pliku");
            }
        }

        [HttpGet]
        public ActionResult GetClients(string searchTerm)
        {
            var results =
                _optimaDbContext.Kontrahenci.Where(x => x.Knt_Nazwa1.ToLower().Contains(searchTerm.ToLower())).ToList();

            if (results.Any())
            {
                var result = Mapper.Instance.Map<List<ClientSearchViewModel>>(results);
                return PartialView("~/Views/Contact/Partial/_SearchClientModal.cshtml", result);
            }

            return PartialView("~/Views/Contact/Partial/_SearchClientModal.cshtml", new List<ClientSearchViewModel>());
        }

        private void SetViewParams(string sortOrder, int page, int pageSize)
        {
            ViewBag.SortParam = sortOrder;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
        }

        internal List<SelectListItem> GetUses()
        {
            if (_users.Count == 0)
            {
                _users = _hermesDataContext.Users.Select(x => new SelectListItem
                {
                    Text = x.FirstName + " " + x.LastName,
                    Value = x.Id.ToString()
                }).ToList();
            }
            return _users;
        }
    }
}
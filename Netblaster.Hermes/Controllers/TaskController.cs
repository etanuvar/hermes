using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using Netblaster.Hermes.DAL;
using Netblaster.Hermes.DAL.Extensions;
using Netblaster.Hermes.DAL.Model;
using Netblaster.Hermes.DAL.Model.Enums;
using Netblaster.Hermes.DAL.Optima;
using Netblaster.Hermes.WebUI.Controllers.Base;
using Netblaster.Hermes.WebUI.Filters;
using Netblaster.Hermes.WebUI.Helpers;
using Netblaster.Hermes.WebUI.Models.Client;
using Netblaster.Hermes.WebUI.Models.Task;
using PagedList;

namespace Netblaster.Hermes.WebUI.Controllers
{
    [Authorize]
    [CustomActionFilter]
    public class TaskController : BaseController
    {
        private readonly int _defaultPage = 1;
        private readonly int _defaultPageSize = 25;
        private readonly Entities _optimaDbContext;
        private readonly HermesDataContext _hermesDataContext;
        private List<SelectListItem> _groups;
        private List<SelectListItem> _users;

        public TaskController()
        {
            _optimaDbContext = new Entities();
            _hermesDataContext = new HermesDataContext();
            _groups = new List<SelectListItem>();
            _users = new List<SelectListItem>();
        }

        [HttpGet]
        public ActionResult Index()
        {
            SetHeaders("Zadania", "Lista wszystkich zadań", "Index", "Task", "/Task/Add", "Dodaj nowe zadanie");

            var taskList = new List<TaskItem>();
            if (CurrentUser.HasClaim("Administrator"))
            {
                taskList = _hermesDataContext.TaskItems.Where(x => x.Group.IsActive).ToList();
            }
            else
            {
                foreach (var group in CurrentUser.UserGroups.Where(x => x.Group.IsActive))
                {
                    var tasksForCurrentGroup = _hermesDataContext.TaskItems.Where(x => x.GroupId == group.GroupId);
                    if (tasksForCurrentGroup.Any())
                    {
                        taskList.AddRange(tasksForCurrentGroup);
                    }
                }
            }

            var tasksDto = taskList.Select(x => new TaskListDto()
            {
                Group = x.Group,
                GroupId = x.GroupId,
                CreateDate = x.CreateDate,
                CreatedBy = x.CreatedBy,
                DeadlineDate = x.DeadlineDate,
                CreatedById = x.CreatedById,
                Note = x.Note,
                Title = x.Title,
                EndDate = x.EndDate,
                Id = x.Id,
                ItemStatus = x.ItemStatus
            }).ToList();

            SetViewParams("CreateDate", _defaultPage, _defaultPageSize);
            var listData = tasksDto.ToPagedList(_defaultPage, _defaultPageSize);

            var userGroups = GetGroups();
            var users = GetUses();

            var viewModel = new TaskListViewModel()
            {
                ListData = listData,
                FilterBox = new TaskFilterDto(userGroups, users)
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(TaskFilterDto filterBox)
        {
            SetHeaders("Zadania", "Lista wszystkich zadań", "Index", "Task");

            var taskList = new List<TaskItem>();
            if (CurrentUser.HasClaim("Administrator"))
            {
                taskList = _hermesDataContext.TaskItems.Where(x => x.Group.IsActive).ToList();
            }
            else
            {
                foreach (var group in CurrentUser.UserGroups.Where(x => x.Group.IsActive))
                {
                    var tasksForCurrentGroup = _hermesDataContext.TaskItems.Where(x => x.GroupId == group.GroupId);
                    if (tasksForCurrentGroup.Any())
                    {
                        taskList.AddRange(tasksForCurrentGroup);
                    }
                }
            }

            var tasksDto = taskList.Select(x => new TaskListDto()
            {
                Group = x.Group,
                GroupId = x.GroupId,
                CreateDate = x.CreateDate,
                CreatedBy = x.CreatedBy,
                CreatedById = x.CreatedById,
                Note = x.Note,
                Title = x.Title,
                EndDate = x.EndDate,
                DeadlineDate = x.DeadlineDate,
                Id = x.Id,
                ItemStatus = x.ItemStatus
            }).ToList();

            var dataFormatter = new TaskDataFormat(filterBox);
            SetViewParams(dataFormatter.SortParam, dataFormatter.Page, dataFormatter.PageSize);

            ViewBag.PageSize = dataFormatter.PageSize;
            var listData = dataFormatter.GetFormattedData(tasksDto)
                .ToPagedList(dataFormatter.Page, dataFormatter.PageSize);

            return PartialView("_PagedListPartial", listData);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var newTaskViewModel = new AddTaskViewModel
            {
                Groups = GetGroups(),
                CreatedBy = CurrentUser,
                CreatedById = CurrentUser.Id,
                CreateDate = DateTime.Now,
                DeadlineDate = DateTime.Now,
                CreateTime = $"{DateTime.Now.Hour:D2}:{DateTime.Now.Minute:D2}",
                UniqueId = Guid.NewGuid().ToString()
            };

            return View(newTaskViewModel);
        }   

        [HttpPost]
        public ActionResult Add(AddTaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var createdByUser = _hermesDataContext.Users.Find(viewModel.CreatedById);
                var selectedGroup = _hermesDataContext.Groups.Find(viewModel.SelectedGroupId);
                var domainModel = Mapper.Instance.Map<TaskItem>(viewModel);

                domainModel.CreateDate = domainModel.CreateDate.AddHours(int.Parse(viewModel.CreateTime.Split(':')[0]));
                domainModel.CreateDate = domainModel.CreateDate.AddMinutes(int.Parse(viewModel.CreateTime.Split(':')[1]));

                domainModel.DeadlineDate = domainModel.DeadlineDate.AddHours(int.Parse(viewModel.EndTime.Split(':')[0]));
                domainModel.DeadlineDate = domainModel.DeadlineDate.AddMinutes(int.Parse(viewModel.EndTime.Split(':')[1]));

                domainModel.Group = selectedGroup;
                domainModel.GroupId = viewModel.SelectedGroupId;
                domainModel.ItemStatus = TaskItemStatus.Pending;

                _hermesDataContext.TaskItems.Add(domainModel);
                _hermesDataContext.SaveChanges();

                if (viewModel.subItemElement != null && viewModel.subItemElement.Length > 0)
                {
                    foreach (var subItem in viewModel.subItemElement)
                    {
                        _hermesDataContext.TaskSubItems.Add(new TaskSubItem()
                        {
                            TaskItemId = domainModel.Id,
                            Text = subItem,
                            IsFinished = false,
                        });
                    }
                    _hermesDataContext.SaveChanges();
                }

                if (!string.IsNullOrEmpty(viewModel.UniqueId))
                {
                    var uploadFileDirectory = WebConfigurationManager.AppSettings["uploadDirectory"];
                    var baseDir = Path.Combine(uploadFileDirectory, viewModel.UniqueId);

                    if (Directory.Exists(baseDir))
                    {
                        foreach (var file in Directory.GetFiles(baseDir))
                        {
                            var binary = System.IO.File.ReadAllBytes(file);
                            var mime = MimeMapping.GetMimeMapping(file);

                            var newAttachment = new Attachment()
                            {
                                BinaryData = binary,
                                TaskItemId = domainModel.Id,
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

                //add history
                var newItem = new TaskDetail()
                {
                    ApplicationUserId = CurrentUser.Id,
                    CreateDate = DateTime.Now,
                    TaskDetailType = TaskDetailType.TaskHistory,
                    TaskItemId = domainModel.Id,
                    Text = $"Zadanie utworzone przez {CurrentUser.DisplayName}."
                };

                _hermesDataContext.TaskDetails.Add(newItem);
                _hermesDataContext.SaveChanges();
                //

                var templateFile = Server.MapPath(Url.Content("~/Content/template.txt"));
                var msg = EmailHelper.GenerateNewTaskMessage($"http://{Request.Url.Authority}", templateFile, CurrentUser, domainModel);
                EmailHelper.SendEmailToGroup(msg, selectedGroup);

                return RedirectToAction("Index");
            }
            viewModel.Groups = GetGroups();
            viewModel.CreatedBy = CurrentUser;
            viewModel.CreatedById = CurrentUser.Id;
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult OnlyMy(int? selected = null)
        {
            var myTasks = _hermesDataContext.TaskItems.Where(x => x.CreatedById == CurrentUser.Id).AsEnumerable();

            var tasksDto = Mapper.Instance.Map<IEnumerable<TaskListDto>>(myTasks);
            var viewModel = new MyTasksViewModel()
            {
                Tasks = tasksDto,
                SelectedId = selected
            };
            return View(viewModel);
        }


        private void SetViewParams(string sortOrder, int page, int pageSize)
        {
            ViewBag.SortParam = sortOrder;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
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

        public ActionResult SetAsDone(int id)
        {
            var task = _hermesDataContext.TaskItems.Find(id);

            task.ItemStatus = TaskItemStatus.Done;
            _hermesDataContext.SaveChanges();

            //add history
            var newItem = new TaskDetail()
            {
                ApplicationUserId = CurrentUser.Id,
                CreateDate = DateTime.Now,
                TaskDetailType = TaskDetailType.TaskHistory,
                TaskItemId = id,
                Text = $"Zadanie oznaczone jako wykonane przez {CurrentUser.DisplayName}."
            };

            _hermesDataContext.TaskDetails.Add(newItem);
            _hermesDataContext.SaveChanges();
            //
            return RedirectToAction("Details", new { id = task.Id });
        }

        public ActionResult SetInProgress(int id)
        {
            var task = _hermesDataContext.TaskItems.Find(id);
            task.ItemStatus = TaskItemStatus.InProgress;
            _hermesDataContext.SaveChanges();

            //add history
            var newItem = new TaskDetail()
            {
                ApplicationUserId = CurrentUser.Id,
                CreateDate = DateTime.Now,
                TaskDetailType = TaskDetailType.TaskHistory,
                TaskItemId = id,
                Text = $"Zadanie podjęte do wykonania przez {CurrentUser.DisplayName}."
            };

            _hermesDataContext.TaskDetails.Add(newItem);
            _hermesDataContext.SaveChanges();
            //

            return RedirectToAction("Details", new {id = task.Id});
        }

        public ActionResult SetRejected(int id)
        {
            var task = _hermesDataContext.TaskItems.Find(id);
            task.ItemStatus = TaskItemStatus.Rejected;
            _hermesDataContext.SaveChanges();

            //add history
            var newItem = new TaskDetail()
            {
                ApplicationUserId = CurrentUser.Id,
                CreateDate = DateTime.Now,
                TaskDetailType = TaskDetailType.TaskHistory,
                TaskItemId = id,
                Text = $"Zadanie odrzucone przez {CurrentUser.DisplayName}."
            };

            _hermesDataContext.TaskDetails.Add(newItem);
            _hermesDataContext.SaveChanges();
            //

            return RedirectToAction("Details", new { id = task.Id });
        }

        [HttpPost]
        public async Task<ActionResult> FilterMyTasks(string searchFilter)
        {
            var myTasks = _hermesDataContext.TaskItems.Where(x => x.GroupId != null && CurrentUser.UserGroups.Select(y => y.GroupId).Contains(x.GroupId.Value) && x.Title.Contains(searchFilter)).AsEnumerable();
            var tasksDto = Mapper.Instance.Map<IEnumerable<TaskListDto>>(myTasks);

            return PartialView("_MyTasksPartial", tasksDto);
        }

        public async Task<ActionResult> Details(int id)
        {
            var task = await _hermesDataContext.CalendarComponents.OfType<TaskItem>().FirstOrDefaultAsync(x => x.Id == id);
            var viewModel = Mapper.Instance.Map<TaskDetailsViewModel>(task);

            ViewBag.CurrentUser = CurrentUser;

            viewModel.Users = _hermesDataContext.Users.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.Id
            }).ToList();

            return View(viewModel);
        }

        public async Task<ActionResult> SetSubItem(int id, bool newValue)
        {
            var taskSubItem = await _hermesDataContext.TaskSubItems.FindAsync(id);
            if (taskSubItem != null)
            {
                taskSubItem.IsFinished = newValue;
                taskSubItem.FinishedDate = DateTime.Now;

                await _hermesDataContext.SaveChangesAsync();
                var taskItem = await _hermesDataContext.TaskItems.FindAsync(taskSubItem.TaskItemId);
                if (!taskItem.TaskSubItems.Any(x => x.IsFinished == false))
                {
                    taskItem.ItemStatus = TaskItemStatus.Done;
                    taskItem.EndDate = DateTime.Now;
                }

                if (taskItem.ItemStatus == TaskItemStatus.Done && taskItem.TaskSubItems.Any(x => x.IsFinished == false))
                {
                    taskItem.ItemStatus = TaskItemStatus.InProgress;
                    taskItem.EndDate = null;
                }

                if (taskItem.TaskSubItems.Any(x => x.IsFinished) && taskItem.ItemStatus == TaskItemStatus.Pending)
                {
                    taskItem.ItemStatus = TaskItemStatus.InProgress;
                }

                _hermesDataContext.SaveChanges();

                //add history
                var isFinishedStatus = newValue ? "Wykonane" : "W trakcie";
                var newItem = new TaskDetail()
                {
                    ApplicationUserId = CurrentUser.Id,
                    CreateDate = DateTime.Now,
                    TaskDetailType = TaskDetailType.TaskHistory,
                    TaskItemId = taskSubItem.TaskItemId,
                    Text = $"Zmieniono status pod-zadania - {taskSubItem.Text} na '{isFinishedStatus}' przez {CurrentUser.DisplayName}."
                };

                _hermesDataContext.TaskDetails.Add(newItem);
                _hermesDataContext.SaveChanges();
                //

                var viewModel = Mapper.Instance.Map<TaskDetailsViewModel>(taskItem);
                var result = new TaskItemResult
                {
                    BaseInfo = this.RenderRazorViewToString("Partial/_TaskInfoPartial", viewModel),
                    SubItems = this.RenderRazorViewToString("Partial/_TaskSubItemsPartial", taskItem.TaskSubItems)
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        public async Task<ActionResult> AttachUser(AddUserToTaskVm taskUserVm)
        {
            var newtaskUser = new TaskItemUser()
            {
                ApplicationUserId = taskUserVm.SelectedUserId,
                TaskItemId = taskUserVm.Id
            };

            _hermesDataContext.TaskItemUsers.Add(newtaskUser);
            await _hermesDataContext.SaveChangesAsync();

            return RedirectToAction("Details", new {id = taskUserVm.Id});
        }

        public async Task<ActionResult> AddComment(AddCommentToTaskVm commentVm)
        {
            var newTaskHistory = new TaskDetail()
            {
                ApplicationUserId = CurrentUser.Id,
                CreateDate = DateTime.Now,
                TaskDetailType = TaskDetailType.TaskComment,
                TaskItemId = commentVm.Id,
                Text = commentVm.Comment,
            };

            _hermesDataContext.TaskDetails.Add(newTaskHistory);
            await _hermesDataContext.SaveChangesAsync();

            return RedirectToAction("Details", new { id = commentVm.Id });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LightInject;
using Netblaster.Hermes.DAL;
using Netblaster.Hermes.DAL.Model;
using Netblaster.Hermes.DAL.Optima;
using Netblaster.Hermes.WebUI;
using Netblaster.Hermes.WebUI.Models.Authorize;
using Netblaster.Hermes.WebUI.Models.Client;
using Netblaster.Hermes.BLL.DTOs;
using Netblaster.Hermes.WebUI.Models.Contact;
using Netblaster.Hermes.WebUI.Models.Management;
using Netblaster.Hermes.WebUI.Models.Task;

namespace Netblaster.Hermes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new ServiceContainer();
            container.RegisterControllers();
            container.EnableMvc();


            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
                cfg.CreateMap<Kontrahenci, ClientListViewModel>().ReverseMap();
                cfg.CreateMap<Kontrahenci, ClientListDto>().ReverseMap();
                cfg.CreateMap<Kontrahenci, ClientSearchViewModel>().ReverseMap();
                cfg.CreateMap<Contact, AddContactViewModel>().ReverseMap();
                cfg.CreateMap<TaskItem, AddTaskViewModel>().ReverseMap();
                cfg.CreateMap<TaskItem, TaskListDto>().ReverseMap();
                cfg.CreateMap<Contact, ContactDetailsViewModel>().ReverseMap();
                cfg.CreateMap<TaskItem, TaskDetailsViewModel>().ReverseMap();
                cfg.CreateMap<Group, UserGroupViewModel>().ReverseMap();
            });

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Netblaster.Hermes.BLL.DTOs;
using Netblaster.Hermes.DAL.Model;
using Netblaster.Hermes.DAL.Optima;
using Netblaster.Hermes.WebUI.Models.Client;
using Netblaster.Hermes.WebUI.Models.Contact;

namespace Netblaster.Hermes.WebUI.App_Start
{

    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Kontrahenci, ClientListViewModel>().ReverseMap();
            CreateMap<Kontrahenci, ClientListDto>().ReverseMap();
            CreateMap<Kontrahenci, ClientSearchViewModel>().ReverseMap();

            CreateMap<Contact, AddContactViewModel>().ReverseMap();
            CreateMap<Contact, ContactDetailsViewModel>().ReverseMap();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Netblaster.Hermes.BLL.DTOs;
using Netblaster.Hermes.BLL.DTOs.FilterBoxes;
using Netblaster.Hermes.WebUI.Models.Base;

namespace Netblaster.Hermes.WebUI.Models.Contact
{
    public class ContactListViewModel : BaseListViewModel<ContactListDto, ContactFilterDto>
    {
        public ContactListViewModel()
        {
        }
    }
}
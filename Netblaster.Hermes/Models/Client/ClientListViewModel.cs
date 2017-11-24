using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Netblaster.Hermes.BLL.DTOs;
using Netblaster.Hermes.BLL.DTOs.FilterBoxes;
using Netblaster.Hermes.WebUI.Models.Base;

namespace Netblaster.Hermes.WebUI.Models.Client
{
    public class ClientListViewModel : BaseListViewModel<ClientListDto, ClientFilterDto>
    {
        public ClientListViewModel()
        {
            FilterBox = new ClientFilterDto();
        }
    }
}
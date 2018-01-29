using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Netblaster.Hermes.DAL.Model;

namespace Netblaster.Hermes.WebUI.Models.Task
{
    public class AddSimpleAttachmentToTaskVm
    {
       
        public int Id { get; set; }

        public string attachmentFor { get; set; }


    }
}
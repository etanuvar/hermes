using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Netblaster.Hermes.WebUI.Models.Base
{
    public abstract class BaseListViewModel<T, V>
    {
        public bool InErrorState { get; set; }

        public V FilterBox { get; set; }

        public IPagedList<T> ListData { get; set; }

    }
}
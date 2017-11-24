using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netblaster.Hermes.BLL.DataFormat
{
    public interface IDataFormatter<T, V>
    {
        int Page { get; set; }

        int PageSize { get; set; }

        string SortParam { get; set; }

        V FilterBox { get; set; }

        IEnumerable<T> GetFormattedData(IEnumerable<T> query);
    }
}

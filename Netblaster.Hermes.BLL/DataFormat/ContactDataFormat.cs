using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netblaster.Hermes.BLL.DTOs;
using Netblaster.Hermes.BLL.DTOs.FilterBoxes;
using Netblaster.Hermes.DAL.Optima;

namespace Netblaster.Hermes.BLL.DataFormat
{
    public class ContactDataFormat : IDataFormatter<ContactListDto, ContactFilterDto>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string SortParam { get; set; }

        public ContactFilterDto FilterBox { get; set; }

        private readonly Entities _optimaDataContext;

        public ContactDataFormat(ContactFilterDto filterBox)
        {
            Page = (filterBox != null && filterBox.Page != 0) ? filterBox.Page : 1;
            PageSize = (filterBox != null && filterBox.PageSize != 0) ? filterBox.PageSize : 25;
            SortParam = filterBox?.SortParam ?? "When_desc";

            FilterBox = filterBox;

            _optimaDataContext = new Entities();
        }


        public IEnumerable<ContactListDto> GetFormattedData(IEnumerable<ContactListDto> query)
        {
            if (FilterBox != null)
            {
                if (!string.IsNullOrEmpty(FilterBox.ClientName))
                {
                    var clients = _optimaDataContext.Kontrahenci.Where(x => x.Knt_Nazwa1.ToLower()
                        .Contains(FilterBox.ClientName.ToLower())).Select(x => x.Knt_KntId);

                    query = query.Where(x => clients.Contains(x.KntId));
                }
                if (!string.IsNullOrEmpty(FilterBox.Note))
                {
                    query = query.Where(x => x.Note.ToLower().Contains(FilterBox.Note.ToLower()));
                }
                if (FilterBox.CreateDate.HasValue)
                {
                    query = query.Where(x => x.CreateDate == FilterBox.CreateDate.Value);
                }
                if (!string.IsNullOrEmpty(FilterBox.SelectedContactType))
                {
                    query = query.Where(x => x.SelectedContactType != null && x.SelectedContactType.ToLower()
                        .StartsWith(FilterBox.SelectedContactType.ToLower()));
                }
                if (!string.IsNullOrEmpty(FilterBox.WorkerName))
                {
                    query = query.Where(x => x.ApplicationUser.FirstName.ToLower()
                                                 .Contains(FilterBox.WorkerName.ToLower())
                                             || x.ApplicationUser.LastName.ToLower()
                                                 .Contains(FilterBox.WorkerName.ToLower()));
                }
            }

            switch (SortParam)
            {
                case "ClientName":
                    query = query.OrderBy(s => s.ClientName);
                    break;
                case "ClientName_desc":
                    query = query.OrderByDescending(s => s.ClientName);
                    break;
                case "CreateDate":
                    query = query.OrderBy(s => s.CreateDate);
                    break;
                case "CreateDate_desc":
                    query = query.OrderByDescending(s => s.CreateDate);
                    break;
                case "WorkerName":
                    query = query.OrderBy(s => s.WorkerName);
                    break;
                case "WorkerName_desc":
                    query = query.OrderByDescending(s => s.WorkerName);
                    break;
                case "Note":
                    query = query.OrderBy(s => s.Note);
                    break;
                case "Note_desc":
                    query = query.OrderByDescending(s => s.Note);
                    break;
                case "EndDate":
                    query = query.OrderBy(s => s.EndDate);
                    break;
                case "EndDate_desc":
                    query = query.OrderByDescending(s => s.EndDate);
                    break;
            }

            return query;
        }
    }
}

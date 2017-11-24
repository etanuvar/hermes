using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netblaster.Hermes.BLL.DTOs;
using Netblaster.Hermes.BLL.DTOs.FilterBoxes;

namespace Netblaster.Hermes.BLL.DataFormat
{
    public class ClientDataFormat : IDataFormatter<ClientListDto, ClientFilterDto>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string SortParam { get; set; }

        public ClientFilterDto FilterBox { get; set; }

        public ClientDataFormat(ClientFilterDto filterBox)
        {
            Page = (filterBox != null && filterBox.Page != 0) ? filterBox.Page : 1;
            PageSize = (filterBox != null && filterBox.PageSize != 0) ? filterBox.PageSize : 25;
            SortParam = filterBox?.SortParam ?? "When_desc";

            FilterBox = filterBox;
        }


        public IEnumerable<ClientListDto> GetFormattedData(IEnumerable<ClientListDto> query)
        {
            if (FilterBox != null)
            {
                if (!string.IsNullOrEmpty(FilterBox.Knt_Email))
                {
                    query = query.Where(x => x.Knt_Email != null && x.Knt_Email.ToLower().StartsWith(FilterBox.Knt_Email.ToLower()));
                }
                if (!string.IsNullOrEmpty(FilterBox.Knt_KodPocztowy))
                {
                    query = query.Where(x => x.Knt_KodPocztowy != null && x.Knt_KodPocztowy.StartsWith(FilterBox.Knt_KodPocztowy));
                }
                if (!string.IsNullOrEmpty(FilterBox.Knt_Nazwa1))
                {
                    query = query.Where(x => x.Knt_Nazwa1 != null && x.Knt_Nazwa1.ToLower().StartsWith(FilterBox.Knt_Nazwa1.ToLower()));
                }
                if (!string.IsNullOrEmpty(FilterBox.Knt_Nip))
                {
                    query = query.Where(x => x.Knt_Nip != null && x.Knt_Nip.StartsWith(FilterBox.Knt_Nip));
                }
                if (!string.IsNullOrEmpty(FilterBox.Knt_Powiat))
                {
                    query = query.Where(x => x.Knt_Miasto != null && x.Knt_Miasto.ToLower().StartsWith(FilterBox.Knt_Powiat.ToLower()));
                }
                if (!string.IsNullOrEmpty(FilterBox.Knt_Telefon1))
                {
                    query = query.Where(x => x.Knt_Telefon1 != null && x.Knt_Telefon1.StartsWith(FilterBox.Knt_Telefon1));
                }
                if (!string.IsNullOrEmpty(FilterBox.Knt_Ulica))
                {
                    query = query.Where(x => x.Knt_Ulica != null && x.Knt_Ulica.ToLower().StartsWith(FilterBox.Knt_Ulica.ToLower()));
                }
            }

            switch (SortParam)
            {
                case "Knt_Nazwa1":
                    query = query.OrderBy(s => s.Knt_Nazwa1);
                    break;
                case "Knt_Nazwa1_desc":
                    query = query.OrderByDescending(s => s.Knt_Nazwa1);
                    break;
                case "Knt_Email":
                    query = query.OrderBy(s => s.Knt_Email);
                    break;
                case "Knt_Email_desc":
                    query = query.OrderByDescending(s => s.Knt_Email);
                    break;
                case "Knt_KodPocztowy":
                    query = query.OrderBy(s => s.Knt_KodPocztowy);
                    break;
                case "Knt_KodPocztowy_desc":
                    query = query.OrderByDescending(s => s.Knt_KodPocztowy);
                    break;
                case "Knt_Powiat":
                    query = query.OrderBy(s => s.Knt_Powiat);
                    break;
                case "Knt_Powiat_desc":
                    query = query.OrderByDescending(s => s.Knt_Powiat);
                    break;
                case "Knt_Telefon1":
                    query = query.OrderBy(s => s.Knt_Telefon1);
                    break;
                case "Knt_Telefon1_desc":
                    query = query.OrderByDescending(s => s.Knt_Telefon1);
                    break;
                case "Knt_Ulica":
                    query = query.OrderBy(s => s.Knt_Ulica);
                    break;
                case "Knt_Ulica_desc":
                    query = query.OrderByDescending(s => s.Knt_Ulica);
                    break;
                case "Knt_Nip":
                    query = query.OrderBy(s => s.Knt_Nip);
                    break;
                case "Knt_Nip_desc":
                    query = query.OrderByDescending(s => s.Knt_Nip);
                    break;

            }

            return query;
        }
    }
}

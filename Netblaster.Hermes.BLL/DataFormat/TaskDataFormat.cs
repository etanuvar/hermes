using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netblaster.Hermes.BLL.DTOs;
using Netblaster.Hermes.BLL.DTOs.FilterBoxes;

namespace Netblaster.Hermes.BLL.DataFormat
{
    public class TaskDataFormat : IDataFormatter<TaskListDto, TaskFilterDto>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string SortParam { get; set; }

        public TaskFilterDto FilterBox { get; set; }

        public TaskDataFormat(TaskFilterDto filterBox)
        {
            Page = (filterBox != null && filterBox.Page != 0) ? filterBox.Page : 1;
            PageSize = (filterBox != null && filterBox.PageSize != 0) ? filterBox.PageSize : 25;
            SortParam = filterBox?.SortParam ?? "CreatedBy_desc";

            FilterBox = filterBox;
        }


        public IEnumerable<TaskListDto> GetFormattedData(IEnumerable<TaskListDto> query)
        {
            if (FilterBox != null)
            {
                if (!string.IsNullOrEmpty(FilterBox.CreatedBy))
                {
                    query = query.Where(x => x.CreatedBy.FirstName.ToLower().StartsWith(FilterBox.CreatedBy)
                                             || x.CreatedBy.LastName.ToLower().StartsWith(FilterBox.CreatedBy));
                }
                if (FilterBox.SelectedGroupId.HasValue)
                {
                    query = query.Where(x => x.GroupId != null && x.GroupId == FilterBox.SelectedGroupId.Value);
                }
                if (!string.IsNullOrEmpty(FilterBox.CreatedById))
                {
                    query = query.Where(x => !string.IsNullOrEmpty(x.CreatedById) && x.CreatedById == FilterBox.CreatedById);
                }
                if (!string.IsNullOrEmpty(FilterBox.AffectedTo))
                {
                    query = query.Where(x => x.ApplicationUser.FirstName.ToLower().StartsWith(FilterBox.AffectedTo)
                                             || x.ApplicationUser.LastName.ToLower().StartsWith(FilterBox.AffectedTo));
                }
                if (!string.IsNullOrEmpty(FilterBox.Note))
                {
                    query = query.Where(x => x.Note.ToLower().Contains(FilterBox.Note.ToLower()));
                }
                if (!string.IsNullOrEmpty(FilterBox.Title))
                {
                    query = query.Where(x => x.Title.ToLower().Contains(FilterBox.Title.ToLower()));
                }
                if (FilterBox.CreateDateFrom.HasValue)
                {
                    query = query.Where(x => x.CreateDate.Day >= FilterBox.CreateDateFrom.Value.Day 
                                            && x.CreateDate.Month >= FilterBox.CreateDateFrom.Value.Month 
                                            && x.CreateDate.Year >= FilterBox.CreateDateFrom.Value.Year);
                }
                if (FilterBox.CreateDateTo.HasValue)
                {
                    query = query.Where(x => x.CreateDate.Day <= FilterBox.CreateDateTo.Value.Day
                                             && x.CreateDate.Month <= FilterBox.CreateDateTo.Value.Month
                                             && x.CreateDate.Year <= FilterBox.CreateDateTo.Value.Year);
                }
                if (FilterBox.DeadlineDateFrom.HasValue)
                {
                    query = query.Where(x => x.DeadlineDate.Day >= FilterBox.DeadlineDateFrom.Value.Day
                                             && x.DeadlineDate.Month >= FilterBox.DeadlineDateFrom.Value.Month
                                             && x.DeadlineDate.Year >= FilterBox.DeadlineDateFrom.Value.Year);
                }
                if (FilterBox.DeadlineDateTo.HasValue)
                {
                    query = query.Where(x => x.DeadlineDate.Day <= FilterBox.DeadlineDateTo.Value.Day
                                             && x.DeadlineDate.Month <= FilterBox.DeadlineDateTo.Value.Month
                                             && x.DeadlineDate.Year <= FilterBox.DeadlineDateTo.Value.Year);
                }

                if (FilterBox.EndDateFrom.HasValue)
                {
                    query = query.Where(x => x.EndDate.HasValue && x.EndDate.Value.Day >= FilterBox.EndDateFrom.Value.Day
                                             && x.EndDate.Value.Month >= FilterBox.EndDateFrom.Value.Month
                                             && x.EndDate.Value.Year >= FilterBox.EndDateFrom.Value.Year);
                }
                if (FilterBox.EndDateTo.HasValue)
                {
                    query = query.Where(x => x.EndDate.HasValue && x.EndDate.Value.Day <= FilterBox.EndDateTo.Value.Day
                                             && x.EndDate.Value.Month <= FilterBox.EndDateTo.Value.Month
                                             && x.EndDate.Value.Year <= FilterBox.EndDateTo.Value.Year);
                }
                if (FilterBox.ItemStatus.HasValue)
                {
                    query = query.Where(x => x.ItemStatus == FilterBox.ItemStatus);
                }

                switch (SortParam)
                {
                    case "CreatedBy":
                        query = query.OrderBy(s => s.CreatedBy.LastName);
                        break;
                    case "CreatedBy_desc":
                        query = query.OrderByDescending(s => s.CreatedBy.LastName);
                        break;
                    case "AffectedTo":
                        query = query.OrderBy(s => s.ApplicationUser.LastName);
                        break;
                    case "AffectedTo_desc":
                        query = query.OrderByDescending(s => s.ApplicationUser.LastName);
                        break;
                    case "Note":
                        query = query.OrderBy(s => s.Note);
                        break;
                    case "Note_desc":
                        query = query.OrderByDescending(s => s.Note);
                        break;
                    case "Title":
                        query = query.OrderBy(s => s.Title);
                        break;
                    case "Title_desc":
                        query = query.OrderByDescending(s => s.Title);
                        break;
                    case "CreateDate":
                        query = query.OrderBy(s => s.CreateDate);
                        break;
                    case "CreateDate_desc":
                        query = query.OrderByDescending(s => s.CreateDate);
                        break;
                    case "DeadlineDate":
                        query = query.OrderBy(s => s.DeadlineDate);
                        break;
                    case "DeadlineDate_desc":
                        query = query.OrderByDescending(s => s.DeadlineDate);
                        break;
                    case "EndDate":
                        query = query.OrderBy(s => s.EndDate);
                        break;
                    case "EndDate_desc":
                        query = query.OrderByDescending(s => s.EndDate);
                        break;
                    case "ItemStatus":
                        query = query.OrderBy(s => s.ItemStatus);
                        break;
                    case "ItemStatus_desc":
                        query = query.OrderByDescending(s => s.ItemStatus);
                        break;
                }
            }
            return query;
        }
    }
}

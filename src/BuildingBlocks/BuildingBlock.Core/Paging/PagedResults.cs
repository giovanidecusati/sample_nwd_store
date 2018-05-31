using System;
using System.Collections.Generic;

namespace BuildingBlock.Core.Paging
{
    public class PagedResults<T>
    {
        public PagedResults()
        {

        }

        public PagedResults(int pageNumber, int pageSize, int totalNumberOfRecords, IEnumerable<T> results)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalNumberOfRecords = totalNumberOfRecords;
            Results = results;
            var pageCount = (double)totalNumberOfRecords / pageSize;
            TotalNumberOfPages = (int)Math.Ceiling(pageCount);
        }

        /// <summary>
        /// The page number this page represents.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The size of this page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of pages available.
        /// </summary>
        public int TotalNumberOfPages { get; set; }

        /// <summary>
        /// The total number of records available.
        /// </summary>
        public int TotalNumberOfRecords { get; set; }

        /// <summary>
        /// The records this page represents.
        /// </summary>
        public IEnumerable<T> Results { get; set; }
    }
}
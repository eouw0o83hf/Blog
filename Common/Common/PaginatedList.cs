using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PaginatedList<T> : IReadOnlyList<T>
    {
        /// <summary>
        /// Total number of items
        /// </summary>
        public readonly int TotalCount;

        /// <summary>
        /// Actual data
        /// </summary>
        protected readonly IList<T> UnderlyingList;

        /// <summary>
        /// 0-indexed page
        /// </summary>
        public readonly int Page;

        /// <summary>
        /// Number of items per page
        /// </summary>
        public readonly int ItemsPerPage;

        /// <summary>
        /// Create a new PaginatedList
        /// </summary>
        /// <param name="source">Source query</param>
        /// <param name="page">0-indexed page</param>
        /// <param name="itemsPerPage">Number of items per page</param>
        public PaginatedList(IEnumerable<T> source, int? page = null, int? itemsPerPage = null)
        {
            Page = page ?? 0;
            ItemsPerPage = itemsPerPage ?? 1;

            TotalCount = source.Count();
            UnderlyingList = source.Skip(Page * ItemsPerPage).Take(ItemsPerPage).ToList();
        }

        public T this[int index]
        {
            get { return UnderlyingList[index]; }
        }

        public int Count
        {
            get { return UnderlyingList.Count; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return UnderlyingList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

using System.Collections.Generic;

namespace PaymentManager.Api.Helpers
{
    public class PaginationResult<T>
    {
        public int PageNumber { get; set; }
        public int NumberOfItems { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
    }
}
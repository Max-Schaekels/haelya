using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Domain.Common
{
    public class PaginationQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int Skip => (Page - 1) * PageSize;
    }
}

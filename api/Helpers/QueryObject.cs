using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; }  = null; // tên cột muốn sắp xếp 
        public bool IsDecending { get; set; } = false; //cho biết tăng-true, giảm-false
        public int PageNumber { get; set; } = 1; // cho biết số trang cần chia
        public int PageSize { get; set; } // cho biết số phần tử trong 1 trang
    }
}
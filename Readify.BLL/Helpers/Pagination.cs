﻿namespace Readify.BLL.Helpers
{
    public class Pagination
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}

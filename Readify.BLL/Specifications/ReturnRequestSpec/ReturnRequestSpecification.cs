using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Constants;

namespace Readify.BLL.Specifications.ReturnRequestSpec
{
    public class ReturnRequestSpecification
    {
        private const int MAXPAGESIZE = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = AppConstants.DefaultPageSize;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value;
        }

        private int? _searchById;
        public int? SearchById
        {
            get => _searchById;
            set => _searchById = value;
        }


        private string? _searchByUserId;
        public string? SearchByUserId
        {
            get => _searchByUserId;
            set => _searchByUserId = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private string? _searchByApprovedBy;
        public string? SearchByApprovedBy
        {
            get => _searchByApprovedBy;
            set => _searchByApprovedBy = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private DateTime? _searchByReturnDate;
        public DateTime? SearchByReturnDate
        {
            get => _searchByReturnDate;
            set => _searchByReturnDate = value;
        }


        private int? _searchByBookId;
        public int? SearchByBookId
        {
            get => _searchByBookId;
            set => _searchByBookId = value;
        }

        private int? _searchByBorrowedBookId;
        public int? SearchByBorrowedBookId
        {
            get => _searchByBorrowedBookId;
            set => _searchByBorrowedBookId = value;
        }


        private ReturnRequestStatus? _searchByStatus;
        public ReturnRequestStatus? SearchByStatus
        {
            get => _searchByStatus;
            set => _searchByStatus = value;
        }

    }
}

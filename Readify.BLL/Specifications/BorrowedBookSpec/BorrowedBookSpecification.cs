using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Constants;

namespace Readify.BLL.Specifications.BorrowedBookSpec
{
    public class BorrowedBookSpecification
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

        private int? _searchByBookId;
        public int? SearchByBookId
        {
            get => _searchByBookId;
            set => _searchByBookId = value;
        }

        private string? _searchByUserId;
        public string? SearchByUserId
        {
            get => _searchByUserId;
            set => _searchByUserId = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private string? _searchByConfirmedById;
        public string? SearchByConfirmedById
        {
            get => _searchByConfirmedById;
            set => _searchByConfirmedById = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private DateTime? _searchByBorrowedAt;
        public DateTime? SearchByBorrowedAt
        {
            get => _searchByBorrowedAt;
            set => _searchByBorrowedAt = value;
        }

        private DateTime? _searchByDueDate;
        public DateTime? SearchByDueDate
        {
            get => _searchByDueDate;
            set => _searchByDueDate = value;
        }

        private DateTime? _searchByReturnedAt;
        public DateTime? SearchByReturnedAt
        {
            get => _searchByReturnedAt;
            set => _searchByReturnedAt = value;
        }

        private BorrowedBookStatus? _searchByStatus;
        public BorrowedBookStatus? SearchByStatus
        {
            get => _searchByStatus;
            set => _searchByStatus = value;
        }

    }
}

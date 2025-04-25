using Readify.BLL.Constants;

namespace Readify.BLL.Specifications.BorrowRequestSpec
{
    public class BorrowRequestSpecification
    {
        private const int MAXPAGESIZE = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = AppConstants.DefaultPageSize;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value;
        }

        private string? _searchByUserId;
        public string? SearchByUserId
        {
            get => _searchByUserId;
            set => _searchByUserId = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private int? _searchByBookId;
        public int? SearchByBookId
        {
            get => _searchByBookId;
            set => _searchByBookId = value;
        }

        private BorrowRequestStatus? _searchByStatus;
        public BorrowRequestStatus? SearchByStatus
        {
            get => _searchByStatus;
            set => _searchByStatus = value;
        }
    }
}

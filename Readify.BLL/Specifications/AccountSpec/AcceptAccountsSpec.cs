using Readify.BLL.Constants;

namespace Readify.BLL.Specifications.AccountSpec
{
    public class AcceptAccountsSpec
    {
        private const int MAXPAGESIZE = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = AppConstants.DefaultPageSize;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value;
        }

        private string? _searchByFullname;
        public string? SearchByFullname
        {
            get => _searchByFullname;
            set => _searchByFullname = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        public UserStatus? Status { get; set; }
    }
}

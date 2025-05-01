using Readify.BLL.Constants;

namespace Readify.BLL.Specifications.UserSpec
{
    public class UserSpecification
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

        private string? _searchByUserName;
        public string? SearchByUserName
        {
            get => _searchByUserName;
            set => _searchByUserName = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private string? _searchByPhoneNumber;
        public string? SearchByPhoneNumber
        {
            get => _searchByPhoneNumber;
            set => _searchByPhoneNumber = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private string? _searchByFullName;
        public string? SearchByFullName
        {
            get => _searchByFullName;
            set => _searchByFullName = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private UserStatus? _searchByStatus;
        public UserStatus? SearchByStatus
        {
            get => _searchByStatus;
            set => _searchByStatus = value;
        }
    }
}

using Readify.BLL.Constants;

namespace Readify.BLL.Specifications.LibrarianSpec
{
    public class LibrarianSpecifications
    {
        private const int MAXPAGESIZE = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = AppConstants.DefaultPageSize;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value;
        }

        private string? _searchById;
        public string? SearchById
        {
            get => _searchById;
            set => _searchById = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private string? _searchByEmail;
        public string? SearchByEmail
        {
            get => _searchByEmail;
            set => _searchByEmail = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private string? _searchByFullname;
        public string? SearchByFullname
        {
            get => _searchByFullname;
            set => _searchByFullname = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private string? _searchByPhoneNumber;
        public string? SearchByPhone
        {
            get => _searchByPhoneNumber;
            set => _searchByPhoneNumber = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        public UserStatus? Status { get; set; }

    }
}

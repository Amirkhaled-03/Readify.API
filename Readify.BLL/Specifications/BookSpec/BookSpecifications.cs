using Readify.BLL.Constants;

namespace Readify.BLL.Specifications.BookSpec
{
    public class BookSpecifications
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

        private string? _searchByBookTitle;
        public string? SearchByBookTitle
        {
            get => _searchByBookTitle;
            set => _searchByBookTitle = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private string? _searchByBookAuthor;
        public string? SearchByBookAuthor
        {
            get => _searchByBookAuthor;
            set => _searchByBookAuthor = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private string? _searchByBookISBN;
        public string? SearchByBookISBN
        {
            get => _searchByBookISBN;
            set => _searchByBookISBN = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }

        private int? _searchByBookCategory;
        public int? SearchByBookCategory
        {
            get => _searchByBookCategory;
            set => _searchByBookCategory = value;
        }
    }
}

using Readify.BLL.Constants;

namespace Readify.BLL.Specifications.CategoriesSpec
{
    public class CategoriesSpecification
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

        private string? _searchByName;
        public string? SearchByName
        {
            get => _searchByName;
            set => _searchByName = string.IsNullOrEmpty(value) ? null : value.Trim().ToLower();
        }
    }
}

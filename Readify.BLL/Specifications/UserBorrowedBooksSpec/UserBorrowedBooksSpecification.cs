using Readify.BLL.Constants;

namespace Readify.BLL.Specifications.UserBorrowedBooksSpec
{
    public class UserBorrowedBooksSpecification
    {
        private const int MAXPAGESIZE = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = AppConstants.DefaultPageSize;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value;
        }
    }
}

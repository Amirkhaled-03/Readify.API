namespace Readify.BLL.Validators.BookValidator
{
    public interface IBookValidator
    {
        Task<List<string>> ValidateAddBook(string isbn, string title, string authorName);
        Task<List<string>> ValidateEditBook(int bookId, string isbn, string title, string authorName);
        Task<List<string>> ValidateDeleteBook(int bookId);
    }
}
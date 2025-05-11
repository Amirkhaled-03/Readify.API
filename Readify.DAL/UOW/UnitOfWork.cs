using Readify.DAL.DBContext;
using Readify.DAL.Repositories.BookCategoriesRepo;
using Readify.DAL.Repositories.BookRepo;
using Readify.DAL.Repositories.BorrowedBookRepo;
using Readify.DAL.Repositories.BorrowRequestRepo;
using Readify.DAL.Repositories.CategoriesRepo;
using Readify.DAL.Repositories.ChatRepo;
using Readify.DAL.Repositories.MessageRepo;
using Readify.DAL.Repositories.ReturnRequestRepo;

namespace Readify.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dBContext;
        public IBookRepository BookRepository { get; }
        public IBorrowedBookRepository BorrowedBookRepository { get; }
        public IBorrowRequestRepository BorrowRequestRepository { get; }
        public ICategoriesRepository CategoriesRepository { get; }
        public IBookCategoriesRepository BookCategoriesRepository { get; }
        public IReturnRequestRepository ReturnRequestRepository { get; }
        public IConversationRepository ConversationRepository { get; }
        public IMessageRepository MessageRepository { get; }


        public UnitOfWork(
            ApplicationDBContext dBContext,
            IBookRepository bookRepository,
            IBorrowedBookRepository borrowedBookRepository,
            IBorrowRequestRepository borrowRequestRepository,
            ICategoriesRepository categoriesRepository,
            IBookCategoriesRepository bookCategoriesRepository,
            IReturnRequestRepository returnRequestRepository,
            IConversationRepository conversationRepository,
            IMessageRepository messageRepository
            )
        {
            _dBContext = dBContext;
            BookRepository = bookRepository;
            BorrowedBookRepository = borrowedBookRepository;
            BorrowRequestRepository = borrowRequestRepository;
            CategoriesRepository = categoriesRepository;
            BookCategoriesRepository = bookCategoriesRepository;
            ReturnRequestRepository = returnRequestRepository;
            ConversationRepository = conversationRepository;
            MessageRepository = messageRepository;
        }

        public async Task<int> SaveAsync()
        {
            return await _dBContext.SaveChangesAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Features.BorrowedBooks.DTOs;
using Readify.BLL.Features.BorrowRequest.DTOs;
using Readify.DAL.Entities;

namespace Readify.BLL.Features.BorrowedBooks.Services
{
    public interface IBorrowedBookService
    {
        Task<List<string>> AddBorrowedBookAsync(DAL.Entities.BorrowRequest bookDto);
    }
}

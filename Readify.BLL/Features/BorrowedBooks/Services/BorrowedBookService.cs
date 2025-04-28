using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Readify.BLL.Features.BorrowedBooks.DTOs;
using Readify.BLL.Features.BorrowRequest.DTOs;
using Readify.BLL.Features.JWTToken;
using Readify.BLL.Validators.BorrowRequestValidators;
using Readify.DAL.Entities;
using Readify.DAL.UOW;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Readify.BLL.Features.BorrowedBooks.Services
{
    internal class BorrowedBookService : IBorrowedBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        public BorrowedBookService(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public async Task<List<string>> AddBorrowedBookAsync(DAL.Entities.BorrowRequest bookDto)
        {
            List<string> errors = new List<string>();

            var book = new BorrowedBook
            {
                BookId = bookDto.BookId,
                UserId = bookDto.UserId,
                ConfirmedById = bookDto.ApprovedById,
                BorrowedAt = bookDto.StartDate,
                DueDate = bookDto.EndDate,
            };

            _unitOfWork.BorrowedBookRepository.AddEntity(book);
            int bookaffectedRows = await _unitOfWork.SaveAsync();

            if (bookaffectedRows <= 0)
                errors.Add("An error occurred while adding borrowed book.");

            return errors;

        }
    }
}

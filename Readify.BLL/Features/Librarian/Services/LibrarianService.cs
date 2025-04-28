using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Features.JWTToken;
using Readify.DAL.UOW;

namespace Readify.BLL.Features.Librarian.Services
{
    internal class LibrarianService : ILibrarianService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public LibrarianService(UnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

    }
}

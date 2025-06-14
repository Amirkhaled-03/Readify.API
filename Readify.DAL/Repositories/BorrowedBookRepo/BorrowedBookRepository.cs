﻿using Microsoft.EntityFrameworkCore;
using Readify.DAL.DBContext;
using Readify.DAL.Entities;
using Readify.DAL.Repositories.GenericRepo;

namespace Readify.DAL.Repositories.BorrowedBookRepo
{
    public class BorrowedBookRepository : GenericRepository<BorrowedBook>, IBorrowedBookRepository
    {
        public BorrowedBookRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<BorrowedBook?> GetBorrowedBookByIdAsync(int id)
        {
            return await _dbSet.Where(b => b.Id == id)
                  .AsNoTracking()
                  .Include(b => b.User)
                  .Include(b => b.Book)
                  .ThenInclude(b => b.BookCategories)
                  .ThenInclude(b => b.Category)
                  .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BorrowedBook>> GetUserBorrowedBooksAsync(string userId)
        {
            return await _dbSet
                .Where(bb => bb.UserId == userId)
                .Include(bb => bb.User)
                .Include(bb => bb.Book)
                .ThenInclude(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .ToListAsync();
        }

        public async Task<BorrowedBook> GetUserLastBorrowedBookAsync(string userId)
        {
            return await _dbSet
                    .Include(bb => bb.Book)
                        .ThenInclude(b => b.BookCategories)
                            .ThenInclude(bc => bc.Category)
                    .Where(bb => bb.UserId == userId)
                    .OrderByDescending(bb => bb.BorrowedAt)
                    .FirstOrDefaultAsync();
        }
    }
}

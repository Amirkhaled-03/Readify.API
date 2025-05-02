using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.DAL.Entities;
using Readify.DAL.Repositories.GenericRepo;

namespace Readify.DAL.Repositories.ReturnRequestRepo
{
    public interface IReturnRequestRepository : IGenericRepository<ReturnRequest>
    {
        Task<ReturnRequest?> GetByIdAsync(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readify.BLL.Specifications.BorrowRequestSpec;
using Readify.DAL.Entities;
using Readify.DAL.SpecificationPattern;

namespace Readify.BLL.Specifications.ChatSpec
{
    internal class ChatSpecificationImpl : BaseSpecification<Conversation>
    {
        public ChatSpecificationImpl(ChatSpecification specification)
            : base(c =>
                (string.IsNullOrEmpty(specification.SearchByUserId) || c.UserId.ToString() == specification.SearchByUserId) &&
                (!specification.SearchById.HasValue || c.Id == specification.SearchById.Value)
                )
        {
            ApplyPagination(specification.PageSize * (specification.PageIndex - 1), specification.PageSize);

            AddInclude(c => c.User);

            ApplyNoTracking();
        }

    }
}

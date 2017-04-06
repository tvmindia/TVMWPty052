using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IReviewRepository
    {
        List<ProductReview> GetAllReviews(string Condition,string FromDate,string ToDate);
    }
}

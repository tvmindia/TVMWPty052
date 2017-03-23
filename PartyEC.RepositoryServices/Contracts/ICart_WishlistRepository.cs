using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface ICart_WishlistRepository
    {
        List<Cart_Wishlist> GetAllCustomerCartWishlistSummary();
    }
}

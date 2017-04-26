using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
    public interface ICart_WishlistBusiness
    {

        List<Customer> GetAllCustomerCartWishlistSummary();

        List<ShoppingCart> GetCustomerShoppingCart(int customerID);

        List<Wishlist> GetCustomerWishlist(int customerID);
        

    }
}

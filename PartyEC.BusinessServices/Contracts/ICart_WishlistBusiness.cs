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

        List<Cart_Wishlist> GetAllCustomerCartWishlistSummary();

        List<Cart_Wishlist> GetCustomerShoppingCart(int customerID);

        List<Cart_Wishlist> GetCustomerWishlist(int customerID);
        

    }
}

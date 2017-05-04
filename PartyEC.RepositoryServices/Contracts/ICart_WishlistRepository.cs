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
        List<Customer> GetAllCustomerCartWishlistSummary();
        
        List<ShoppingCart> GetCustomerShoppingCart(int customerID,int locationID);

        List<Wishlist> GetCustomerWishlist(int customerID, string CurrentDate);

        OperationsStatus AddProductToCart(ShoppingCart cartObj);
    }
}

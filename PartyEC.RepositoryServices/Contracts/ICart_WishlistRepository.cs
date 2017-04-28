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
        
        List<ShoppingCart> GetCustomerShoppingCart(int customerID);

        List<Wishlist> GetCustomerWishlist(int customerID);

        OperationsStatus AddProductToCart(ShoppingCart cartObj);
    }
}

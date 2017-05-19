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

        List<ShoppingCart> GetCustomerShoppingCart(ShoppingCart cartObj);

        List<Wishlist> GetCustomerWishlist(int customerID, string CurrentDate);

        OperationsStatus AddProductToCart(ShoppingCart cartObj);
        OperationsStatus RemoveProductFromCart(ShoppingCart cartObj);
        OperationsStatus UpdateShoppingCartStatus(int CartID);


    }
}

using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class Cart_WishlistBusiness
    {
        private ICart_WishlistRepository _cartWishlistRepository;

        public Cart_WishlistBusiness(ICart_WishlistRepository cartWishlistRepository)
        {
            _cartWishlistRepository = cartWishlistRepository;
        }
    }
}
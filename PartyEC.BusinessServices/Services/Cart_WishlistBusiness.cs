﻿using PartyEC.BusinessServices.Contracts;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;

namespace PartyEC.BusinessServices.Services
{
    public class Cart_WishlistBusiness: ICart_WishlistBusiness
    {
        private ICart_WishlistRepository _cartWishlistRepository;

        public Cart_WishlistBusiness(ICart_WishlistRepository cartWishlistRepository)
        {
            _cartWishlistRepository = cartWishlistRepository;
        }

        public List<Cart_Wishlist> GetAllCustomerCartWishlistSummary()
        {
            List<Cart_Wishlist> cartWishlist = null;
            try
            {
                cartWishlist = _cartWishlistRepository.GetAllCustomerCartWishlistSummary();

            }
            catch (Exception)
            {

            }
            return cartWishlist;
        }
    }
}
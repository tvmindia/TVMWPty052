using PartyEC.BusinessServices.Contracts;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using System.Xml;

namespace PartyEC.BusinessServices.Services
{
    public class Cart_WishlistBusiness: ICart_WishlistBusiness
    {
        private ICart_WishlistRepository _cartWishlistRepository;
        private ICommonBusiness _commonBusiness;

        public Cart_WishlistBusiness(ICart_WishlistRepository cartWishlistRepository, ICommonBusiness commonBusiness)
        {
            _cartWishlistRepository = cartWishlistRepository;
            _commonBusiness = commonBusiness;
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

        public List<Cart_Wishlist> GetCustomerShoppingCart(int customerID)
        {
            List<Cart_Wishlist> cartlist = null;
            try
            {
                cartlist = _cartWishlistRepository.GetCustomerShoppingCart(customerID);

                for (int i = 0; i < cartlist.Count; i++)
                {
                   cartlist[i].AttributeValues = GetAttributeValueFromXML(cartlist[i].ProductSpecXML);
                }
            }
            catch (Exception)
            {

            }
            return cartlist;
        }         

        public List<Cart_Wishlist> GetCustomerWishlist(int customerID)
        {
            List<Cart_Wishlist> wishlist = null;
            try
            {
                wishlist = _cartWishlistRepository.GetCustomerWishlist(customerID);
                for (int i = 0; i < wishlist.Count; i++)
                {
                    wishlist[i].AttributeValues = GetAttributeValueFromXML(wishlist[i].ProductSpecXML);
                }
            }
            catch (Exception)
            {

            }
            return wishlist;
        }


        private List<AttributeValues> GetAttributeValueFromXML(string XML)
        {
            List<AttributeValues> myAttributeValueList = null;
            try
            {
                myAttributeValueList = new List<AttributeValues>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(XML);

                XmlNodeList dataNodes = xmlDoc.SelectNodes("//options");
                foreach (XmlNode node in dataNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        AttributeValues myAttributeValues = new AttributeValues();
                        myAttributeValues.Name = childNode.Name;
                        myAttributeValues.Value = childNode.InnerXml;

                        myAttributeValueList.Add(myAttributeValues);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return myAttributeValueList;

        }

    }
}
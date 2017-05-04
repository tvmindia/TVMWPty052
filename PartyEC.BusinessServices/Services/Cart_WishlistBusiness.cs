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
        private IAttributesRepository _attributesRepository;

        public Cart_WishlistBusiness(ICart_WishlistRepository cartWishlistRepository, ICommonBusiness commonBusiness, IAttributesRepository attributesRepository)
        {
            _cartWishlistRepository = cartWishlistRepository;
            _commonBusiness = commonBusiness;
            _attributesRepository = attributesRepository;
        }

        public List<Customer> GetAllCustomerCartWishlistSummary()
        {
            List<Customer> Customerlist = null;
            try
            {
                Customerlist = _cartWishlistRepository.GetAllCustomerCartWishlistSummary();

            }
            catch (Exception)
            {

            }
            return Customerlist;
        }

        public List<ShoppingCart> GetCustomerShoppingCart(int customerID,int locationID)
        {
            List<ShoppingCart> cartlist = null;
            try
            {
                cartlist = _cartWishlistRepository.GetCustomerShoppingCart(customerID, locationID);
              if (cartlist!=null)
                for (int i = 0; i < cartlist.Count; i++)
                {
                    if (cartlist[i].ProductSpecXML != null)
                        cartlist[i].AttributeValues = GetAttributeValueFromXML(cartlist[i].ProductSpecXML);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cartlist;
        }         

        public List<Wishlist> GetCustomerWishlist(int customerID, string CurrentDate)
        {
            List<Wishlist> wishlist = null;
            try
            {
                wishlist = _cartWishlistRepository.GetCustomerWishlist(customerID,CurrentDate);
                if (wishlist != null)
                    for (int i = 0; i < wishlist.Count; i++)
                        {
                             if (wishlist[i].ProductSpecXML != null)
                                wishlist[i].AttributeValues = GetAttributeValueFromXML(wishlist[i].ProductSpecXML);
                        }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return wishlist;
        }

        private List<AttributeValues> GetAttributeValueFromXML(string XML)
        {
            List<AttributeValues> myAttributeValueList = null;
            List<Attributes> attributelist = null;
            try
            {
                attributelist = _attributesRepository.GetAllAttributes();//Selecting Attributes List

                myAttributeValueList = new List<AttributeValues>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(XML);

                XmlNodeList dataNodes = xmlDoc.SelectNodes("//options");
                foreach (XmlNode node in dataNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        AttributeValues myAttributeValues = new AttributeValues();                        
                        //checking the attribute list for Caption by Comparing XML Node
                        List<Attributes> Matchingattribute = attributelist.Where(attr => attr.Name == childNode.Name).ToList();
                        if (Matchingattribute.Count>0)
                        {//if Matching attribute found its captions will be taken for display
                            myAttributeValues.Caption = Matchingattribute[0].Caption;
                            myAttributeValues.Value = childNode.InnerXml;
                        }
                        else
                        {//if Matching attribute not found Childnode Name will be taken for display
                            myAttributeValues.Caption = childNode.Name;
                            myAttributeValues.Value = childNode.InnerXml;
                        }
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

        public OperationsStatus AddProductToCart(ShoppingCart cartObj)
        {
            OperationsStatus OSatObj = null;
            try
            {
                OSatObj = _cartWishlistRepository.AddProductToCart(cartObj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OSatObj;
        }
    }
}
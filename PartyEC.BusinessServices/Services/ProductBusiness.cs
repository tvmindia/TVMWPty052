using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class ProductBusiness:IProductBusiness
    {
        private IProductRepository _productRepository;

        public ProductBusiness(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts(Product productObj)
        {
            List<Product> productlist = null;
            try
            {
                productlist = _productRepository.GetAllProducts(productObj);

            }
            catch(Exception)
            {

            }
            return productlist;
        }

        public OperationsStatus InsertProduct(Product productObj)
        {
            
              return _productRepository.InsertProduct(productObj);
            
        }

    }
}
﻿using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.RepositoryServices.Contracts
{
   public interface IProductRepository
   {
       List<Product> GetAllProducts(Product productObj);
       OperationsStatus InsertProduct(Product productObj);
        Product GetProduct(int ProductID, OperationsStatus Status);
   }
}
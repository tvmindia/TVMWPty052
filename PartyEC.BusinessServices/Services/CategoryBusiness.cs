using PartyEC.BusinessServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;

namespace PartyEC.BusinessServices.Services
{
    public class CategoryBusiness : ICategoryBusiness
    {
        #region ConstructorInjection

        private ICategoryRepository _categoryRepository;

        public CategoryBusiness(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        #endregion ConstructorInjection      


       
        public List<Category> GetAllCategory()
        {
            List<Category> Categorylist = null;

            try {

                Categorylist = _categoryRepository.GetAllCategory();
            }
            catch (Exception)
            {

            }
            return Categorylist;
        }

        public Category GetCategory(int CategoryID, OperationsStatus Status)
        {
            Category myCategory = null;
            try
            {
                myCategory= _categoryRepository.GetCategory(CategoryID,Status);
            }
            catch (Exception)
            {

            }
            return myCategory;
        }

        public OperationsStatus InsertCategory(Category CategoryObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj = _categoryRepository.InsertCategory(CategoryObj);
            }
            catch (Exception)
            {

            }
            return operationsStatusObj;
        }

        public OperationsStatus UpdateCategory(Category CategoryObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                _categoryRepository.UpdateCategory(CategoryObj);
            }
            catch (Exception)
            {

            }
            return operationsStatusObj;
        }

        public OperationsStatus DeleteCategory(int CategoryID, OperationsStatus Status)
        {
            try
            {
            }
            catch (Exception)
            {

            }
            return Status;
        }

    }
}
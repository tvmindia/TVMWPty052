using PartyEC.BusinessServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;

namespace PartyEC.BusinessServices.Services
{
    public class CategoriesBusiness : ICategoriesBusiness
    {
        #region ConstructorInjection

        private ICategoriesRepository _categoryRepository;
        private IMasterRepository _masterRepository;

        public CategoriesBusiness(ICategoriesRepository categoryRepository,IMasterRepository masterRepository)
        {
            _categoryRepository = categoryRepository;
            _masterRepository = masterRepository;
        }

        #endregion ConstructorInjection      


       
        public List<Categories> GetAllCategory()
        {
            List<Categories> Categorylist = null;

            try {

                Categorylist = _categoryRepository.GetAllCategory();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Categorylist;
        }

        public Categories GetCategory(int CategoryID)
        {
            Categories myCategory = null;
            try
            {
                myCategory= _categoryRepository.GetCategory(CategoryID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myCategory;
        }
        public OperationsStatus InsertImageCategory(Categories CategoryObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                if (CategoryObj.URL != "" && CategoryObj.URL != null)
                {
                    OtherImages otherImgObj = new OtherImages();
                    otherImgObj.URL = CategoryObj.URL;
                    otherImgObj.ImageType = "Category";
                    otherImgObj.LogDetails = CategoryObj.commonObj;
                    operationsStatusObj = _masterRepository.InsertImage(otherImgObj);
                    CategoryObj.ImageID = operationsStatusObj.ReturnValues.ToString();
                }
                operationsStatusObj = _categoryRepository.UpdateCategory(CategoryObj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationsStatusObj;
        }
        public OperationsStatus InsertCategory(Categories CategoryObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                   operationsStatusObj = _categoryRepository.InsertCategory(CategoryObj);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationsStatusObj;
        }

        public OperationsStatus UpdateCategory(Categories CategoryObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj=_categoryRepository.UpdateCategory(CategoryObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationsStatusObj;
        }

        public OperationsStatus DeleteCategory(int CategoryID)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                bool ExistOrNot = _categoryRepository.ExistOrNot(CategoryID);
                if(ExistOrNot)
                {
                    operationsStatusObj = new OperationsStatus();
                    operationsStatusObj.StatusMessage = "Can't Delete, Child Exist!";
                    operationsStatusObj.StatusCode = 0;
                }
                else
                {
                    operationsStatusObj = _categoryRepository.DeleteCategory(CategoryID);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationsStatusObj;
        }
        public OperationsStatus UpdatePositionNo(Categories CategoryObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj = _categoryRepository.UpdatePositionNo(CategoryObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationsStatusObj;
        }

    }
}
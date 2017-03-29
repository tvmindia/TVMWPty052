using PartyEC.BusinessServices.Contracts;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using System.Data.SqlClient;
using System.Data;

namespace PartyEC.BusinessServices.Services
{
    public class MasterBusiness: IMasterBusiness
    {
        
        private IMasterRepository _masterRepository;

        public MasterBusiness(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            List<Manufacturer> manufacturelist = null;
            try
            {
                manufacturelist = _masterRepository.GetAllManufacturers();

            }
            catch (Exception)
            {

            }
            return manufacturelist;
        }

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> supplierlist = null;
            try
            {
                supplierlist = _masterRepository.GetAllSuppliers();

            }
            catch (Exception)
            {

            }
            return supplierlist;
        }
        public List<OrderStatusMaster> GetAllOrderStatus()
        {
            List<OrderStatusMaster> orderStatuslist = null;
            try
            {
                orderStatuslist = _masterRepository.GetAllOrderStatus();

            }
            catch (Exception)
            {

            }
            return orderStatuslist;
        }
        public List<OtherImages> GetAllStickers()
        {
            List<OtherImages> otherImagesList = null;
            try
            {
                otherImagesList = _masterRepository.GetAllStickers();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return otherImagesList;
        }
    }
}
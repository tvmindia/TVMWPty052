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

        public List<Country> GetAllCountries()
        {
            List<Country> countrylist = null;
            try
            {
                countrylist = _masterRepository.GetAllCountries();

            }
            catch (Exception)
            {

            }
            return countrylist;
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
            catch (Exception ex)
            {
                throw ex;
            }
            return otherImagesList;
        }

        #region Suppliers

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> supplierlist = null;
            try
            {
                supplierlist = _masterRepository.GetAllSuppliers();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return supplierlist;
        }

        public Supplier GetSupplier(int SupplierID, OperationsStatus Status)
        {
            Supplier mySupplier = null;
            try
            {
                mySupplier = _masterRepository.GetSupplier(SupplierID, Status);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mySupplier;
        }

        public OperationsStatus InsertSupplier(Supplier supplierObj)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.InsertSupplier(supplierObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperationsStatusObj;
        }

        public OperationsStatus UpdateSupplier(Supplier supplierObj)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.UpdateSupplier(supplierObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperationsStatusObj;
        }

        public OperationsStatus DeleteSupplier(int supplierID)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.DeleteSupplier(supplierID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperationsStatusObj;
        }

        #endregion Suppliers

        #region ShippingLocation

        public List<ShippingLocations> GetAllShippingLocation()
        {
            List<ShippingLocations> ShippingLocationlist = null;
            try
            {
                ShippingLocationlist = _masterRepository.GetAllShippingLocation();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ShippingLocationlist;
        }

        public ShippingLocations GetShippingLocation(int ShippingLocationID, OperationsStatus Status)
        {
            ShippingLocations myShippingLocation = null;
            try
            {
                myShippingLocation = _masterRepository.GetShippingLocation(ShippingLocationID, Status);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myShippingLocation;
        }

        public OperationsStatus InsertShippingLocation(ShippingLocations shipping_locObj)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.InsertShippingLocation(shipping_locObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperationsStatusObj;
        }

        public OperationsStatus UpdateShippingLocation(ShippingLocations supplierObj)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.UpdateShippingLocation(supplierObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperationsStatusObj;
        }

        public OperationsStatus DeleteShippingLocation(int ShippingLocationID)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.DeleteShippingLocation(ShippingLocationID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperationsStatusObj;
        }

        #endregion ShippingLocation

        #region SupplierLocations

        public List<SupplierLocations> GetAllSupplierLocations()
        {
            List<SupplierLocations> SupplierLocationslist = null;
            try
            {
                SupplierLocationslist = _masterRepository.GetAllSupplierLocations();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SupplierLocationslist;
        }

        public SupplierLocations GetSupplierLocations(int SupplierLocationsID, OperationsStatus Status)
        {
            SupplierLocations mySupplierLocations = null;
            try
            {
                mySupplierLocations = _masterRepository.GetSupplierLocations(SupplierLocationsID, Status);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mySupplierLocations;
        }

        public OperationsStatus InsertSupplierLocations(SupplierLocations sup_locObj)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.InsertSupplierLocations(sup_locObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperationsStatusObj;
        }

        public OperationsStatus UpdateSupplierLocations(SupplierLocations sup_locObj)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.UpdateSupplierLocations(sup_locObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperationsStatusObj;
        }

        public OperationsStatus DeleteSupplierLocations(int SupplierLocationsID)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.DeleteSupplierLocations(SupplierLocationsID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperationsStatusObj;
        }

        #endregion SupplierLocations

        #region Manufacturers
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
         

        #endregion Manufacturers




    }
}
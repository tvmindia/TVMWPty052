﻿using PartyEC.BusinessServices.Contracts;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

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

        public List<PaymentStatusMaster> GetAllPaymentStatus()
        {
            List<PaymentStatusMaster> paymentStatuslist = null;
            try
            {
                paymentStatuslist = _masterRepository.GetAllPaymentStatus();

            }
            catch (Exception)
            {

            }
            return paymentStatuslist;
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
        public List<EventStatusMaster> GetAllEventStatus()
        {
            List<EventStatusMaster> eventStatuslist = null;
            try
            {
                eventStatuslist = _masterRepository.GetAllEventStatus();

            }
            catch (Exception)
            {

            }
            return eventStatuslist;
        }
        public List<BookingStatusMaster> GetAllBookingStatus()
        {
            List<BookingStatusMaster> Statuslist = null;
            try
            {
                Statuslist = _masterRepository.GetAllBookingStatus();

            }
            catch (Exception)
            {

            }
            return Statuslist;
        }

        public List<QuotationStatusMaster> GetAllQuotationStatus()
        {
            List<QuotationStatusMaster> quotationStatuslist = null;
            try
            {
                quotationStatuslist = _masterRepository.GetAllQuotationStatus();

            }
            catch (Exception)
            {

            }
            return quotationStatuslist;
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
        public OperationsStatus InsertEventsLog(EventsLog eventLogObj)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.InsertEventsLog(eventLogObj);
            }
            catch (Exception)
            {
            }
            return OperationsStatusObj;
        }
        public List<EventsLog> GetEventsLog(int ID,string ParentType)
        {
            List<EventsLog> EventsLogLists = null;
            try
            {
                EventsLogLists = _masterRepository.GetEventsLog(ID,ParentType);

            }
            catch (Exception)
            {

            }
            return EventsLogLists;
        }
        public List<Graph> GetWeeklySalesDetails()
        {
            List<Graph> WeeklySalesDeatails = null;
            try
            {
                WeeklySalesDeatails = _masterRepository.GetWeeklySalesDetails().OrderBy(p => p.Label).ToList();
                if(WeeklySalesDeatails!=null)
                {
                    foreach (var i in WeeklySalesDeatails)
                    {
                        DateTime dt = FirstDateOfWeek(int.Parse(DateTime.Now.Year.ToString()), int.Parse(i.Label));
                        int weekOfMonth = GenerateWeekNumber(dt) - GenerateWeekNumber(dt.AddDays(1 - dt.Day)) + 1;
                        string Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dt.Month);
                        i.Label = Month + " Week " + weekOfMonth;
                    }
                }             
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return WeeklySalesDeatails;
        }
        public static int GenerateWeekNumber(DateTime dt)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }

        public List<Graph> GetRootCategoryWiseSalesDetail()
        {
            List<Graph> GraphList = null;
            try
            {
                GraphList = _masterRepository.GetRootCategoryWiseSalesDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GraphList;
        }


        #region Suppliers

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> supplierlist = null;
            try
            {
                supplierlist= _masterRepository.GetAllSuppliers();
                supplierlist = supplierlist==null?null: supplierlist.OrderBy(sup=>sup.Name).ToList();

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

                manufacturelist = manufacturelist == null ? null : manufacturelist.OrderBy(manu => manu.Name).ToList();
            }
            catch (Exception)
            {

            }
            return manufacturelist;
        }

        public Manufacturer GetManufacturer(int ManufacturerID, OperationsStatus Status)
        {
            Manufacturer myManufacturer = null;
            try
            {
                myManufacturer = _masterRepository.GetManufacturer(ManufacturerID, Status);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myManufacturer;
        }

        public OperationsStatus InsertManufacturer(Manufacturer ManufacturerObj)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.InsertManufacturer(ManufacturerObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperationsStatusObj;
        }

        public OperationsStatus UpdateManufacturer(Manufacturer ManufacturerObj)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.UpdateManufacturer(ManufacturerObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperationsStatusObj;
        }

        public OperationsStatus DeleteManufacturer(int ManufacturerID)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _masterRepository.DeleteManufacturer(ManufacturerID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperationsStatusObj;
        }

        #endregion Manufacturers




    }
}
﻿using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IMasterBusiness
    {
        List<Manufacturer> GetAllManufacturers();
        Manufacturer GetManufacturer(int ManufacturerID, OperationsStatus Status);
        OperationsStatus InsertManufacturer(Manufacturer ManufacturerObj);
        OperationsStatus UpdateManufacturer(Manufacturer ManufacturerObj);
        OperationsStatus DeleteManufacturer(int ManufacturerID);
        OperationsStatus InsertEventsLog(EventsLog eventsLogObj);
        List<EventsLog> GetEventsLog(int ID,string ParentType);
        List<Country> GetAllCountries();
        List<PaymentStatusMaster> GetAllPaymentStatus();
        List<OrderStatusMaster> GetAllOrderStatus();
        List<EventStatusMaster> GetAllEventStatus();
        List<QuotationStatusMaster> GetAllQuotationStatus();
        List<BookingStatusMaster> GetAllBookingStatus();
        List<OtherImages> GetAllStickers();

        //Supplier
        List<Supplier> GetAllSuppliers();
        Supplier GetSupplier(int SupplierID, OperationsStatus Status);
        OperationsStatus InsertSupplier(Supplier supplierObj);
        OperationsStatus UpdateSupplier(Supplier supplierObj);
        OperationsStatus DeleteSupplier(int supplierID);

        //ShippingLocations
        List<ShippingLocations> GetAllShippingLocation();
        ShippingLocations GetShippingLocation(int ShippingLocationID, OperationsStatus Status);
        OperationsStatus InsertShippingLocation(ShippingLocations shipping_locObj);
        OperationsStatus UpdateShippingLocation(ShippingLocations shipping_locObj);
        OperationsStatus DeleteShippingLocation(int ShippingLocationID);

        //SupplierLocations
        List<SupplierLocations> GetAllSupplierLocations();
        SupplierLocations GetSupplierLocations(int ShippingLocationID, OperationsStatus Status);
        OperationsStatus InsertSupplierLocations(SupplierLocations supplier_locObj);
        OperationsStatus UpdateSupplierLocations(SupplierLocations supplier_locObj);
        OperationsStatus DeleteSupplierLocations(int ShippingLocationID);

        //Dashboard
        List<Graph> GetWeeklySalesDetails();
        List<Graph> GetRootCategoryWiseSalesDetail();
    }
}

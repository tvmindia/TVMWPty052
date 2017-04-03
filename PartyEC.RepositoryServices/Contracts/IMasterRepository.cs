using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IMasterRepository
    {
       List<Manufacturer> GetAllManufacturers();
      
       OperationsStatus InsertImage(OtherImages otherimgObj);
        List<OrderStatusMaster> GetAllOrderStatus();
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
    }
}

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

        List<Supplier> GetAllSuppliers();
        Supplier GetSupplier(int SupplierID, OperationsStatus Status);
        OperationsStatus InsertSupplier(Supplier supplierObj);
        OperationsStatus UpdateSupplier(Supplier supplierObj);
        OperationsStatus DeleteSupplier(int supplierID);

        List<ShippingLocations> GetAllShippingLocation();
        ShippingLocations GetShippingLocation(int ShippingLocationID, OperationsStatus Status);
        OperationsStatus InsertShippingLocation(ShippingLocations shipping_locObj);
        OperationsStatus UpdateShippingLocation(ShippingLocations shipping_locObj);
        OperationsStatus DeleteShippingLocation(int ShippingLocationID);
    }
}

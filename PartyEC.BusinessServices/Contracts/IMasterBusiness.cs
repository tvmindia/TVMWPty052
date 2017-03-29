using PartyEC.DataAccessObject.DTO;
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
        List<Supplier> GetAllSuppliers();
        List<OrderStatusMaster> GetAllOrderStatus();
        List<OtherImages> GetAllStickers();
    }
}

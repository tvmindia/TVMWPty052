using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IShipmentBusiness
    {
        List<Shipment> GetAllShipmentHeader();
        List<Shipment> GetShipmentHeader(int ID);
        List<ShipmentDetail> GetAllShipmentDetail(int ID);
    }
}
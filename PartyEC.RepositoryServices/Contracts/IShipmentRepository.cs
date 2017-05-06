using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IShipmentRepository
    {
        List<Shipment> GetAllShipmentHeader();
        List<ShipmentDetail> GetAllShipmentDetail(int ID);
        OperationsStatus InsertShipmentHeader(Shipment shipmentObj);
        OperationsStatus InsertShipmentDetail(ShipmentDetail shipmentDetailObj);
        OperationsStatus UpdateDeliveryStatus(Shipment shipmentObj);
    }
}
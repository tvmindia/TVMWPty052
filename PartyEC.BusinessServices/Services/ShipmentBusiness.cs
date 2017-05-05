using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class ShipmentBusiness:IShipmentBusiness
    {
        private IShipmentRepository _shipmentRepository;
        public ShipmentBusiness(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public List<Shipment> GetAllShipmentHeader()
        {
            return _shipmentRepository.GetAllShipmentHeader();
        }
        public List<Shipment> GetShipmentHeader(int ID)
        {
            List<Shipment> ShipList= _shipmentRepository.GetAllShipmentHeader();
            if (ShipList != null)
            {
                ShipList.Select(t => t.OrderID == ID).ToList();
            }
            return ShipList;
        }
        public List<ShipmentDetail> GetAllShipmentDetail(int ID)
        {
            return _shipmentRepository.GetAllShipmentDetail(ID);
        }
        public OperationsStatus InsertShipment(Shipment shipmentObj)
        {
            OperationsStatus operationstatusObj = new OperationsStatus();
            operationstatusObj = InsertShipmentHeader(shipmentObj);
            if(operationstatusObj.StatusCode==1)
            {
                if(shipmentObj.DetailsList.Count!=0)
                {
                    foreach(var i in shipmentObj.DetailsList)
                    {
                        i.ShipmentID = int.Parse(operationstatusObj.ReturnValues.ToString());
                        i.log = shipmentObj.log;                                               
                        InsertShipmentDetail(i);
                    }
                }
            }
            return operationstatusObj;
        }
        public OperationsStatus InsertShipmentHeader(Shipment shipmentObj)
        {
            return _shipmentRepository.InsertShipmentHeader(shipmentObj);
        }
        public OperationsStatus InsertShipmentDetail(ShipmentDetail shipmentDetailObj)
        {
            return _shipmentRepository.InsertShipmentDetail(shipmentDetailObj);
        }
        public OperationsStatus UpdateDeliveryStatus(Shipment shipmentObj)
        {
            return _shipmentRepository.UpdateDeliveryStatus(shipmentObj);
        }
    }
}
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
    }
}
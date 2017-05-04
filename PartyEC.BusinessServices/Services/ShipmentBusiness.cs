using PartyEC.BusinessServices.Contracts;
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
    }
}
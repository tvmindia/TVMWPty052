using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IAttributesBusiness
    {
        List<Attributes> GetAllAttributes(Attributes attributesObj);
        OperationsStatus InsertAttributes(Attributes attributesObj);
        OperationsStatus UpdateAttributes(Attributes attributesObj);
    }
    public interface IAttributeSetBusiness
    {

    }
}

using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IAttributesRepository
    {

        List<Attributes> GetAllAttributes(Attributes attributesObj);
        OperationsStatus InsertAttributes(Attributes attributesObj);
        OperationsStatus UpdateAttributes(Attributes attributesObj);
    }
    public interface IAttributeSetRepository
    {
        
    }
    public interface IAttributeToSetLinksRepository
    {
        OperationsStatus InsertAttributeSetLink(AttributeSetLink obj);
        OperationsStatus DeleteAttributeSetLink(string ID);
    }
}

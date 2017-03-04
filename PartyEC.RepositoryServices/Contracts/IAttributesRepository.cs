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
        List<AttributeValues> GetProductAttributeStructure(int AttributeSetID, string Type);
    }
    public interface IAttributeSetRepository
    {

    }
    public interface IAttributeToSetLinks
    {

    }
}

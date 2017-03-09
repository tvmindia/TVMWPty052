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

        List<Attributes> GetAllAttributes();
        Attributes GetAttributes(int attributeID, OperationsStatus Status);
        OperationsStatus InsertAttributes(Attributes attributesObj);
        OperationsStatus UpdateAttributes(Attributes attributesObj);
        OperationsStatus DeleteAttributes(int attributeID, OperationsStatus Status);
        List<AttributeValues> GetAttributeContainer(int AttributeSetID, string Type);
    }
    public interface IAttributeSetRepository
    {
        List<AttributeSet> GetAllAttributeSet();
        OperationsStatus InsertAttributeSet(AttributeSet obj);
        OperationsStatus UpdateAttributeSet(AttributeSet obj,int ID);
    }
    public interface IAttributeToSetLinksRepository
    {
        OperationsStatus InsertAttributeSetLink(AttributeSetLink obj);
        OperationsStatus DeleteAttributeSetLink(int ID);
    }
}

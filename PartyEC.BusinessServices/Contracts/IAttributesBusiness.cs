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
        List<Attributes> GetAllAttributes();
        Attributes GetAttributes(int AttributeID, OperationsStatus Status);
        OperationsStatus InsertAttributes(Attributes attributesObj);
        OperationsStatus UpdateAttributes(Attributes attributesObj);
        OperationsStatus DeleteAttributes(int AttributeID);
        List<Attributes> GetAllAttributeBySet(int AttributeSetID,bool IsConfigurable, string EntityType = "Product");


    }
    public interface IAttributeSetBusiness
    {
        List<AttributeSet> GetAllAttributeSet();
        OperationsStatus InsertAttributeSet(AttributeSet attributeSetObj);
        OperationsStatus UpdateAttributeSet(AttributeSet attributeSetObj,int ID);
        OperationsStatus DeleteAttributeSet(int ID);
    }
    public interface IAttributeToSetLinks
    {
        OperationsStatus TreeViewUpdateAttributeSetLink(List<AttributeSetLink> TreeView,int ID);
    }

}

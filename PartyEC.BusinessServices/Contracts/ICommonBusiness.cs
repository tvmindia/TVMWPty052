using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
    public interface ICommonBusiness
    {
        UA GetUA();
        void UpdateUA(UA uaObj);
        DateTime GetCurrentDateTime();
        void SendMessage(string Msg, string MobileNos);


    }
}

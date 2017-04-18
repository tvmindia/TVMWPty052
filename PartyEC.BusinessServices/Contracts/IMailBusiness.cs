﻿using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IMailBusiness
    {
        bool Send(Mail mailObj);
        bool SendMail(Mail mailObj);
        //void SendMail(Mail mailObj);
         Task SendMailAsync(Mail mailObj);
    }
}
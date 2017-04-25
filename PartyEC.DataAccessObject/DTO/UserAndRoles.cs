﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class User    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public int ProfileImageId { get; set; }
        public LogDetails logDetails { get; set; }
    }
    public class Role
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public LogDetails logDetails { get; set; }
    }
}
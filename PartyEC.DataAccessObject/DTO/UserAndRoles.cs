using System;
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
        public Guid? ProfileImageId { get; set; }
        public int UserRoleLinkID { get; set; }
        public int RoleID { get; set; }
        public string RoleList { get; set; }
        public Role RoleObj { get; set;}
        public LogDetails logDetails { get; set; }
    }
    public class Role
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public LogDetails logDetails { get; set; }
    }
}
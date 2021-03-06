﻿using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IDynamicUIRepository
    {
        List<Menu> GetAllMenues();
        List<Treeview> GetTreeListForAttrSet(string ID);
        List<Treeview> GetTreeListForAttr(string ID);
        List<Treeview> GetTreeListForCategories();

    }
}

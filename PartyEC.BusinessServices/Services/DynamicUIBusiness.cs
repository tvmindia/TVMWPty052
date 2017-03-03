﻿using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class DynamicUIBusiness : IDynamicUIBusiness
    {
        private IDynamicUIRepository _dynamicUIRepository;
        /// <summary>
        /// Constructor Injection:-Getting IDynamicUIBusiness implementing object
        /// </summary>
        /// <param name="dynamicUIBusiness"></param>
        public DynamicUIBusiness(IDynamicUIRepository dynamicUIRespository)
        {
            _dynamicUIRepository = dynamicUIRespository;
        }

        public List<Menu> GetAllMenues()
        {
            try
            {
                return _dynamicUIRepository.GetAllMenues();
            }
            catch (Exception)
            {
                throw;
            }
            

        }
        public List<JsTreeNode> GetTreeList()
        {
            List<JsTreeNode> nodesList = new List<JsTreeNode>();
            try
            {
                List<Treeview> TreeViewList = _dynamicUIRepository.GetTreeList();

                foreach (var i in TreeViewList)
                {
                    if (i.level.ToString() == "0")
                    {
                        JsTreeNode rootNode = new JsTreeNode();
                        rootNode.id = i.ID.ToString();
                        rootNode.text = i.Name.ToString();
                        rootNode.children = new List<JsTreeNode>();
                        foreach (var j in TreeViewList)
                        {
                            if ((j.ParentID.ToString() == i.ID.ToString())&&(j.level.ToString()=="1"))
                            {

                                JsTreeNode Node = new JsTreeNode();
                                Node.id = j.ID.ToString();
                                Node.text = j.Name.ToString();
                                Node.children = new List<JsTreeNode>();
                                rootNode.children.Add(Node);
                            }
                        }
                        nodesList.Add(rootNode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return nodesList;
        }




    }
}
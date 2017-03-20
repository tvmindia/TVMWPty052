using PartyEC.BusinessServices.Contracts;
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
        public List<JsTreeNode> GetTreeListAttributes(string ID)
        {
            List<JsTreeNode> nodesList = new List<JsTreeNode>();
            try
            {
                List<Treeview> TreeViewList = _dynamicUIRepository.GetTreeListForAttr(ID);

                foreach (var i in TreeViewList)
                {
                    if (i.level.ToString() == "0")
                    {
                        JsTreeNode rootNode = new JsTreeNode();
                        rootNode.id = i.ID.ToString();
                        rootNode.text = i.Name.ToString();
                        rootNode.icon = i.icon.ToString();
                        rootNode.children = new List<JsTreeNode>();
                        foreach (var j in TreeViewList)
                        {
                            if ((j.ParentID.ToString() == i.ID.ToString())&&(j.level.ToString()=="1"))
                            {

                                JsTreeNode Node = new JsTreeNode();
                                Node.id = j.ID.ToString();
                                Node.text = j.Name.ToString();
                                Node.icon = j.icon.ToString();
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

        public List<JsTreeNode> GetTreeListAttributeSet(string ID)
        {
            List<JsTreeNode> nodesList = new List<JsTreeNode>();
            try
            {
                List<Treeview> TreeViewList = _dynamicUIRepository.GetTreeListForAttrSet(ID);

                foreach (var i in TreeViewList)
                {
                    if (i.level.ToString() == "0")
                    {
                        JsTreeNode rootNode = new JsTreeNode();
                        rootNode.id = i.ID.ToString();
                        rootNode.text = i.Name.ToString();
                        rootNode.icon = i.icon.ToString();
                        rootNode.children = new List<JsTreeNode>();
                        foreach (var j in TreeViewList)
                        {
                            if ((j.ParentID.ToString() == i.ID.ToString()) && (j.level.ToString() == "1"))
                            {

                                JsTreeNode Node = new JsTreeNode();
                                Node.id = j.ID.ToString();
                                Node.text = j.Name.ToString();
                                Node.icon = j.icon.ToString();
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

        public List<JsTreeNode> GetTreeListCategories()
        {
            List<JsTreeNode> nodesList = new List<JsTreeNode>();
            try
            {
                List<Treeview> TreeViewList = _dynamicUIRepository.GetTreeListForCategories();

                foreach (var i in TreeViewList)
                {
                    if (i.level.ToString() == "0")
                    {
                        JsTreeNode rootNode = new JsTreeNode();
                        rootNode.id = i.ID.ToString();
                        rootNode.text = i.Name.ToString();
                        rootNode.icon = "fa fa fa-list";
                        rootNode.children = new List<JsTreeNode>();
                        LookForChildNode(TreeViewList, rootNode, i);                       
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
        private void LookForChildNode(List<Treeview> TreeViewList,JsTreeNode rootNode,Treeview i)
        {
            foreach (var j in TreeViewList)
            {
                if (j.ParentID.ToString() == i.ID.ToString())
                {

                    JsTreeNode Node = new JsTreeNode();
                    Node.id = j.ID.ToString();
                    Node.text = j.Name.ToString();
                    Node.icon = "fa fa-tags";
                    Node.children = new List<JsTreeNode>();
                    
                    rootNode.children.Add(Node);
                    LookForChildNode(TreeViewList, Node, j);
                }
                
            }
        }



    }
}
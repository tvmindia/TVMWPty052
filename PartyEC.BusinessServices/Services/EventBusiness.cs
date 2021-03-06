﻿using PartyEC.BusinessServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;

namespace PartyEC.BusinessServices.Services
{
    public class EventBusiness : IEventBusiness
    {
        #region ConstructorInjection

        private IEventRepositry _eventRepository;
        private ICategoriesRepository _categoryRepositry;
        private IMasterRepository _masterRepository;
        public EventBusiness(IEventRepositry eventRepository,ICategoriesRepository categoryRepositry,IMasterRepository masterRepository)
        {
            _eventRepository = eventRepository;
            _categoryRepositry = categoryRepositry;
            _masterRepository = masterRepository;
        }
        #endregion ConstructorInjection

        #region Method

        public List<Event> GetAllEvents()
        {
            List<Event> Eventlist = null;
            List<Categories> Categorylist = null;
            try
            {
                Eventlist = _eventRepository.GetAllEvents();
                Categorylist = _categoryRepositry.GetAllCategory();//All Category List for Id and Name  
                if (Eventlist!=null)
                {
                    foreach (Event eventObj in Eventlist)
                    {
                        string CategoryNames = "";
                        int[] Cat_Id = eventObj.RelatedCategoriesCSV.Split(',').Select(Int32.Parse).ToArray();
                        foreach (int Id in Cat_Id)
                        {
                            List<Categories> CategoryResult = null;
                            CategoryResult = Categorylist.Where(t => t.ID == Id).ToList(); //Find category with Id
                            if (CategoryResult.Count > 0)
                            {
                                CategoryNames = CategoryNames + CategoryResult[0].Name + ",";//Adding Category Name to String
                            }
                        }
                        //Removing the last ',' character and assigning values to EventList Object
                        eventObj.RelatedCategoriesCSV = CategoryNames.TrimEnd(',');
                    }
                }              

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Eventlist;
        }

        public Event GetEvent(int EventID, OperationsStatus Status)
        {
            Event myEvent = null;
            try
            {
                myEvent = _eventRepository.GetEvent(EventID, Status);

            }
            catch (Exception)
            {

            }
            return myEvent;
        }

        public OperationsStatus InsertEventTypes(Event EventObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj= _eventRepository.InsertEventTypes(EventObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return operationsStatusObj;
        }

        public OperationsStatus UpdateEvent(Event EventObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj = _eventRepository.UpdateEvent(EventObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return operationsStatusObj;
        }

        public OperationsStatus DeleteEvent(int EventID)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj = _eventRepository.DeleteEvent(EventID);
            }
            catch (Exception)
            {
            }
            return operationsStatusObj;
        }

        public OperationsStatus InsertImageEvents(Event EventObj)
        {

            OperationsStatus operationsStatusObj = null;
            try
            {
                if (EventObj.URL != "" && EventObj.URL != null)
                {
                    OtherImages otherImgObj = new OtherImages();
                    otherImgObj.URL = EventObj.URL;
                    otherImgObj.ImageType = "Event";
                    otherImgObj.LogDetails = EventObj.commonObj;
                    operationsStatusObj = _masterRepository.InsertImage(otherImgObj);
                    EventObj.EventImageID = operationsStatusObj.ReturnValues.ToString();
                }
                operationsStatusObj = _eventRepository.UpdateEvent(EventObj);

            }
            catch (Exception)
            {

            }
            return operationsStatusObj;
        }
        #region DeleteOtherImage
        public OperationsStatus DeleteOtherImage(string imageID,string type)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj = _masterRepository.DeleteOtherImage(imageID,type);
            }
            catch(Exception)
            {

            }
            return operationsStatusObj;
        }
        #endregion DeleteOtherImage
        #endregion Method
    }
}
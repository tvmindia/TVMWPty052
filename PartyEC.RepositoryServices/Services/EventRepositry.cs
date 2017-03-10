using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.RepositoryServices.Services
{
    public class EventRepositry : IEventRepositry

    {
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public EventRepositry(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory

        #region Methods

        public List<Event> GetAllEvents()
        {
            List<Event> Eventlist = null;
            try
            {

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

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myEvent;
        }

        public OperationsStatus InsertEvent(Event EventObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {

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

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return operationsStatusObj;
        }

        public OperationsStatus DeleteEvent(int EventID, OperationsStatus Status)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return operationsStatusObj;
        }

        #endregion Methods
    }
}
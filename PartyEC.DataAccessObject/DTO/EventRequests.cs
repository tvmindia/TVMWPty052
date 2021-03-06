﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class EventRequests
    {
        public int ID { get; set; }
        public string EventReqNo { get; set; }
        public string EventType { get; set; }
        public string EventTitle  { get; set; }
        public string EventDateTime { get; set; }
        public string EventTime { get; set; }
        public   int NoOfPersons { get; set; }
        public decimal Budget  { get; set; }
        public string LookingFor  { get; set; }
        public string RequirementSpec  { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ContactName  { get; set; }
        public string Email  { get; set; }
        public string Phone  { get; set; }
        public string ContactType  { get; set; }
        public string Message  { get; set; }
        public string AdminRemarks  { get; set; }
        public int EventStatus { get; set; }

        public string FollowUpDate { get; set; } 
        public string CurrencyCode  { get; set; }
        public decimal CurrencyRate  { get; set; }
        public decimal TotalAmt  { get; set; }
        public decimal TotalTaxAmt  { get; set; }
        public decimal TotalDiscountAmt  { get; set; }

        public int Updateflag { get; set; }

        public string Comments { get; set; }
        public string ParentType { get; set; }
        public string CommentDate { get; set; }
        public string PrevComment { get; set; }
        public string EventDesc { get; set; }

        public LogDetails logDetailsObj { get; set; }

        public OperationsStatus operationsStatusObj { get; set; } // For Insert,Update,Delete
    }
}
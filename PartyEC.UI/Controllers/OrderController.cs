using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.CustomAttributes;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    [CustomAuthenticationFilter]
    public class OrderController : Controller
    {
        Const ConstObj = new Const();
        #region Constructor_Injection
        IOrderBusiness _orderBusiness;
        ICommonBusiness _commonBusiness;
        IMasterBusiness _masterBusiness;
        IMailBusiness _mailBusiness;
        IProductBusiness _productBusiness;
        IInvoiceBusiness _invoiceBusiness;
        IShipmentBusiness _shipmentBusiness;
        public OrderController(IProductBusiness productBusiness, IOrderBusiness orderBusiness, ICommonBusiness commonBusiness,IMasterBusiness masterBusiness,IMailBusiness mailBusiness, IInvoiceBusiness invoiceBusiness,IShipmentBusiness shipmentBusiness)
        {
            _orderBusiness = orderBusiness;
            _commonBusiness = commonBusiness;
            _masterBusiness = masterBusiness;
            _mailBusiness = mailBusiness;
            _productBusiness = productBusiness;
            _invoiceBusiness = invoiceBusiness;
            _shipmentBusiness = shipmentBusiness;
        }
        #endregion Constructor_Injection
        // GET: Order
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult Index()
        {
            OrderViewModel order = null;
            try
            {
                order = new OrderViewModel();
                List<SelectListItem> selectListItem = new List<SelectListItem>();
                List<SelectListItem> selectListPaymentStatus = new List<SelectListItem>();
                //Country Drop down bind
                List<CountryViewModel> CounrtyList = Mapper.Map<List<Country>, List<CountryViewModel>>(_masterBusiness.GetAllCountries());
                foreach (CountryViewModel ccl in CounrtyList)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = ccl.Name,
                        Value = ccl.Code,
                        Selected = false
                    });
                }
                order.Countries = selectListItem;
                List<PaymentStatusViewModel> paymentstatusListVM = Mapper.Map<List<PaymentStatusMaster>, List<PaymentStatusViewModel>>(_masterBusiness.GetAllPaymentStatus());
                foreach (PaymentStatusViewModel pvm in paymentstatusListVM)
                {
                    selectListPaymentStatus.Add(new SelectListItem
                    {
                        Text = pvm.Description,
                        Value = pvm.Code.ToString(),
                        Selected = false
                    });
                }
                order.PaymentStatusList = selectListPaymentStatus;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return View(order);
        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetOrderHeader()
        {
            try
            {
                List<OrderViewModel> OrderHeaderList = Mapper.Map<List<Order>, List<OrderViewModel>>(_orderBusiness.GetOrderHeader());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = OrderHeaderList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetShipmentHeader(string ID)
        {
            try
            {
                List<ShipmentViewModel> ShipmentHeaderList = Mapper.Map<List<Shipment>, List<ShipmentViewModel>>(_shipmentBusiness.GetShipmentHeader(int.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = ShipmentHeaderList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetAllShipmentDetail(string ID)
        {
            try
            {
                List<ShipmentDetailViewModel> ShipmentDetailList = Mapper.Map<List<ShipmentDetail>, List<ShipmentDetailViewModel>>(_shipmentBusiness.GetAllShipmentDetail(int.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = ShipmentDetailList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetAllOrdersList(string ID)
        {
            try
            {
                List<OrderDetailViewModel> OrderHeaderList = Mapper.Map<List<OrderDetail>, List<OrderDetailViewModel>>(_orderBusiness.GetAllOrdersList(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = OrderHeaderList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetOrderDetails(string ID)
        {
            try
            {
                OrderViewModel OrderList = null;
                if (ID != "")
                {
                    OrderList = Mapper.Map < Order,  OrderViewModel >( _orderBusiness.GetOrderDetails(ID));
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = OrderList });
                }
                return JsonConvert.SerializeObject(new { Result = "OK", Records = OrderList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            //return JsonConvert.SerializeObject(new { Result = "OK", Records = NodeList });
        }
       
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetProductsListtoAdd(string ID)
        {
            try
            {
                    List<ProductDetailViewModel> productList = Mapper.Map<List<ProductDetail>, List<ProductDetailViewModel>>(_productBusiness.GetAllProductDetail(int.Parse(ID)));

                    return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetEventsLog(string ID)
        {
            try
            {
                List<EventsLogViewModel> eventsLogList = Mapper.Map<List<EventsLog>, List<EventsLogViewModel>>(_masterBusiness.GetEventsLog(int.Parse(ID), "Order"));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = eventsLogList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetOrderSummary(string ID)
        {
            try
            {
                OrderViewModel OrderViewModelObj = Mapper.Map<Order, OrderViewModel>(_orderBusiness.GetOrderSummary(int.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = OrderViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetOrderExcludesShip(string ID)
        {
            try
            {
                List<OrderDetailViewModel> orderDetailList = Mapper.Map<List<OrderDetail>, List<OrderDetailViewModel>>(_orderBusiness.GetOrderExcludesShip(int.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = orderDetailList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetMailTemplate(string ID)
        {
            try
            {
                 string TemplateBody = _mailBusiness.GetMailTemplate(int.Parse(ID));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = TemplateBody });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetInvoiceTemplate(string ID)
        {
            try
            {
                string TemplateBody = _mailBusiness.GetInvoiceTemplate(int.Parse(ID));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = TemplateBody });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }
        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string CancelOrder(OrderViewModel orderObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                orderObj.OrderStatus = "4";
                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_orderBusiness.CancelOrder(Mapper.Map < OrderViewModel, Order > (orderObj)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string SendMail(MailViewModel mailObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = new OperationsStatusViewModel();
            OrderViewModel orderObj = new OrderViewModel();
            try
            {

                bool status = _mailBusiness.Send(Mapper.Map<MailViewModel, Mail>(mailObj));
                if(status)
                {
                    orderObj.ID = mailObj.OrderID;
                    orderObj.OrderStatus = "2";
                    _orderBusiness.CancelOrder(Mapper.Map<OrderViewModel,Order>(orderObj));
                    OperationsStatusViewModelObj.StatusCode = 1;
                    OperationsStatusViewModelObj.StatusMessage = ConstObj.OrderconfirmSuccess;
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
                }
                else
                {
                    OperationsStatusViewModelObj.StatusCode = 0;
                    OperationsStatusViewModelObj.StatusMessage = ConstObj.MailNotsend;
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Records = OperationsStatusViewModelObj });
                }
                
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string SendMailInvoice(MailViewModel mailObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = new OperationsStatusViewModel();
            try
            {

                bool status = _mailBusiness.Send(Mapper.Map<MailViewModel, Mail>(mailObj));
                if (status)
                {
                    OperationsStatusViewModelObj.StatusCode = 1;
                    OperationsStatusViewModelObj.StatusMessage = ConstObj.InvoiceSendSuccess;
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
                }
                else
                {
                    OperationsStatusViewModelObj.StatusCode = 0;
                    OperationsStatusViewModelObj.StatusMessage = ConstObj.MailNotsend;
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Records = OperationsStatusViewModelObj });
                }

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string UpdateEventsLog(OrderViewModel orderObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            bool Mailstatus = false;
                try
                {
                   
                    orderObj.EventsLogViewObj.commonObj = new LogDetailsViewModel();
                    orderObj.EventsLogViewObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                    orderObj.EventsLogViewObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                    if(orderObj.mailViewModelObj.CustomerEmail!=""&&orderObj.EventsLogViewObj.CustomerNotifiedYN==true)
                {
                    orderObj.mailViewModelObj.OrderNo = orderObj.EventsLogViewObj.ParentID;
                    orderObj.mailViewModelObj.OrderComment = orderObj.EventsLogViewObj.Comment;
                    Mailstatus = _mailBusiness.Send(Mapper.Map<MailViewModel, Mail>(orderObj.mailViewModelObj));
                    orderObj.EventsLogViewObj.CustomerNotifiedYN = Mailstatus;
                }
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.InsertEventsLog(Mapper.Map<EventsLogViewModel, EventsLog>(orderObj.EventsLogViewObj)));
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }

                if (OperationsStatusViewModelObj.StatusCode == 1)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
                }
        }
        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string InsertReviseOrder(OrderDetailViewModel OrderDetailViewModelObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {

                OrderDetailViewModelObj.commonObj = new LogDetailsViewModel();
                OrderDetailViewModelObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                OrderDetailViewModelObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_orderBusiness.InsertReviseOrder(Mapper.Map<OrderDetailViewModel, OrderDetail>(OrderDetailViewModelObj)));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

            if (OperationsStatusViewModelObj.StatusCode == 1)
            {
                return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
            }
        }
        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string InsertInvoice(InvoiceViewModel InvoiceViewObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {

                InvoiceViewObj.LogDetails = new LogDetailsViewModel();
                InvoiceViewObj.LogDetails.CreatedBy = _commonBusiness.GetUA().UserName;
                InvoiceViewObj.LogDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();
                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_invoiceBusiness.InsertInvoice(Mapper.Map<InvoiceViewModel, Invoice>(InvoiceViewObj)));
                
            if (OperationsStatusViewModelObj.StatusCode == 1)
            {
                return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
            }
            else
            {
                return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
            }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public bool CheckInvoicedOrNot(string ID)
        {
            try
            {
               return _invoiceBusiness.CheckInvoicedOrNot(int.Parse(ID));
                
           }
            catch(Exception ex)
            {
                return false;
            }
        }
        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string InsertShipment(ShipmentViewModel shipmentViewModelObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {

                shipmentViewModelObj.log = new LogDetailsViewModel();
                shipmentViewModelObj.log.CreatedBy = _commonBusiness.GetUA().UserName;
                shipmentViewModelObj.log.CreatedDate = _commonBusiness.GetCurrentDateTime();
                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_shipmentBusiness.InsertShipment(Mapper.Map<ShipmentViewModel, Shipment>(shipmentViewModelObj)));

                if (OperationsStatusViewModelObj.StatusCode == 1)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }
        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string UpdateDeliveryStatus(ShipmentViewModel shipmentViewModelObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {

                shipmentViewModelObj.log = new LogDetailsViewModel();
                shipmentViewModelObj.log.UpdatedBy = _commonBusiness.GetUA().UserName;
                shipmentViewModelObj.log.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_shipmentBusiness.UpdateDeliveryStatus(Mapper.Map<ShipmentViewModel, Shipment>(shipmentViewModelObj)));

                if (OperationsStatusViewModelObj.StatusCode == 1)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }
        [ValidateAntiForgeryToken]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string UpdateBillingDetails(OrderViewModel orderViewModelObj)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                OperationsStatusViewModel OperationsStatusViewModelObj =Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_orderBusiness.UpdateBillingDetails(Mapper.Map < OrderViewModel, Order >(orderViewModelObj)));
                if (OperationsStatusViewModelObj.StatusCode == 0)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Records = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
                }

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string UpdateShipingDetails(OrderViewModel orderViewModelObj)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_orderBusiness.UpdateShipingDetails(Mapper.Map<OrderViewModel, Order>(orderViewModelObj)));
                if (OperationsStatusViewModelObj.StatusCode == 0)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Records = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
                }

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string UpdateOrderPaymentStatus(Order OrderObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                OrderObj.commonObj = new LogDetails();
                OrderObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                OrderObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_orderBusiness.UpdateOrderPaymentStatus(OrderObj));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #region ChangeButtonStyle
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "Processed":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    ToolboxViewModelObj.cancelbtn.Visible = true;
                    ToolboxViewModelObj.cancelbtn.Title = "Cancel";
                    ToolboxViewModelObj.cancelbtn.Event = "CancelOrder()";
                    break;
                case "InvoiceTemplate":
                    ToolboxViewModelObj.sendbtn.Visible = true;
                    ToolboxViewModelObj.sendbtn.Event = "SendInvoice()";
                    ToolboxViewModelObj.sendbtn.Title = "Send Invoice";
                    break;
                case "OrderTemplate":
                    ToolboxViewModelObj.sendbtn.Visible = true;
                    ToolboxViewModelObj.sendbtn.Event = "SendOrderConfirmation()";
                    ToolboxViewModelObj.sendbtn.Title = "Send Order";
                    break;
                case "InvoiceRegion":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goBack()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    ToolboxViewModelObj.sendbtn.Visible = true;
                    ToolboxViewModelObj.sendbtn.Event = "ShowTemplatePreviewInvoice()";
                    ToolboxViewModelObj.sendbtn.Title = "Mail";
                    break;
                case "ShipmentRegion":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Title = "Add";
                    ToolboxViewModelObj.addbtn.Event = "AddShipment()";
                    break;
                case "AddShipmentRegion":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goBackShipping()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = "SaveShippingDetails()";
                    break;
                case "OldShipmentDetail":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goBackShipping()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    break;
                case "Cancelled":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    break;
                case "List":
                    ToolboxViewModelObj.backbtn.Visible = false;
                    break;
                case "Edit_List":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    ToolboxViewModelObj.editbtn.Visible = true;
                    ToolboxViewModelObj.editbtn.Title = "Revise";
                    ToolboxViewModelObj.editbtn.Event = "CancelIssue()";
                    ToolboxViewModelObj.cancelbtn.Visible = true;
                    ToolboxViewModelObj.cancelbtn.Title = "Cancel";
                    ToolboxViewModelObj.cancelbtn.Event = "CancelOrder()";
                    ToolboxViewModelObj.sendbtn.Visible = true;
                    ToolboxViewModelObj.sendbtn.Event = "ShowTemplatePreview()";
                    ToolboxViewModelObj.sendbtn.Title = "Mail";
                    break;
                case "Revise":
                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = "InsertNewOrder()";
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "gobackDetails()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    //ToolboxViewModelObj.deletebtn.Visible = true;
                    //ToolboxViewModelObj.deletebtn.Event = "DeleteDemoOrderData()";
                    //ToolboxViewModelObj.deletebtn.Title = "Delete";
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Event = "AddNewRevision()";
                    ToolboxViewModelObj.addbtn.Title = "Add";
                    break;
                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }


        #endregion ChangeButtonStyle
    }
}
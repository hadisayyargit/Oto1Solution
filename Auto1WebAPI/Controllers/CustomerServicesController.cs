using Auto1API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Data.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Auto1API.Controllers
{
    [ApiController]
    //[Route("[controller]/[action]")]
    [Route("[action]")]
    public class CustomerServicesController : ControllerBase
    {

        public Auto1Context Auto1Context { get; set; }
        public CustomerServicesController(Auto1Context _auto1context)
        {
            this.Auto1Context = _auto1context;


        }

        [HttpGet(Name = "GetCustomerService")]
        public IEnumerable<CustomerServiceViewModel> GetCustomerService(long serviceid = -1, long customerid = -1, int vendorid = -1, int postmanid = -1, string beginticketdatetime = "2000-1-1", string endticketdatetime = "2000-1-1",  string servicestatusserie = "255")
        {
            System.FormattableString s3 = $"sp_GetCustomerService @serviceid = {serviceid}, @customerid={customerid}, @vendorid={vendorid}, @postmanid={postmanid},@beginticketdatetime={beginticketdatetime},@endticketdatetime={endticketdatetime},@servicestatusserie={servicestatusserie}";

            List<CustomerServiceViewModel> res = this.Auto1Context.Set<CustomerServiceViewModel>().FromSql(s3).ToList();

            return res;
        }

        [HttpGet(Name = "GetCustomerServiceItem")]
        public IEnumerable<CustomerServiceItemViewModel> GetCustomerServiceItem(long serviceid = -1, long customerid = -1, int manufacturerid = -1, int vendorid = -1, int postmanid = -1, string ticketdatetime="2000-1-1" , string servicedatetime= "2000-1-1", byte servicestatus = 255)
        {
     
            ///stored procedure 
            System.FormattableString s3 = $"sp_GetCustomerServiceItem @serviceid = {serviceid}, @customerid={customerid}, @manufacturerid={manufacturerid}, @vendorid={vendorid}, @postmanid={postmanid},@ticketdatetime={ticketdatetime},@servicedatetime={servicedatetime}";

            List<CustomerServiceItemViewModel> res=new List<CustomerServiceItemViewModel> ();

            try
            {
                res = this.Auto1Context.Set<CustomerServiceItemViewModel>().FromSql(s3).ToList();
            }
            catch (Exception ex)
            {
            }

            return res;

        }

        // POST api/<CustomerServicesController>
        [HttpPost]
        public string AddCustomerService([FromBody] List<CustomerServiceItemViewModel> newitems)
        {
            string res = "";
            CustomerServiceModel newcustomerservice = new CustomerServiceModel();
            List<CustomerServiceItemModel> newserviceitem = new List<CustomerServiceItemModel>();
            CustomerServiceItemModel item1 = new CustomerServiceItemModel();

            int[] vendors = newitems.Select(v => v.VendorId).Distinct().ToArray<int>();

            for (int i = 0; i < vendors.Length; i++)
            {
                var Transaction1 = this.Auto1Context.Database.BeginTransaction();


                newcustomerservice.ServiceId = GetServiceId(newitems[0].CustomerId, vendors[i]);

                //newcustomerservice.ServiceId = 0;
                newcustomerservice.CustomerId = newitems[0].CustomerId;
                newcustomerservice.ServiceType = 0;
                newcustomerservice.ServiceStatus = 0;
                newcustomerservice.TicketDateTime = DateTime.Now;
                newcustomerservice.ServiceDateTime = DateTime.Now;
                newcustomerservice.VendorId = vendors[i];
                newcustomerservice.Latitude = newitems[0].Latitude;
                newcustomerservice.Longitude = newitems[0].Longitude;
                newcustomerservice.Address = newitems[0].Address;

                try
                {
                    if (newcustomerservice.ServiceId == 0)
                    {
                        this.Auto1Context.CustomerService.Add(newcustomerservice);


                        this.Auto1Context.SaveChanges();
                    }


                    long serviceid = newcustomerservice.ServiceId; // Your Identity column ID

                    newserviceitem = new List<CustomerServiceItemModel>();
                    List<CustomerServiceItemViewModel> newitems1;
                    newitems1 = newitems.Where(m => m.VendorId == vendors[i]).ToList();

                    foreach (var item in newitems1)
                    {
                        item1 = new CustomerServiceItemModel();
                        item1.ServiceId = serviceid;
                        item1.Quantity = item.Quantity;
                        item1.PartId = item.PartId;
                        item1.ServiceItemType = item.ServiceItemType;
                        item1.ServiceItemStatus = item.ServiceItemStatus;
                        item1.NetAmount = item.NetAmount;
                        item1.PriceAmount = item.PriceAmount;
                        newserviceitem.Add(item1);

                    }

                    this.Auto1Context.CustomerServiceItem.AddRange(newserviceitem);
                    this.Auto1Context.SaveChanges();
                    Transaction1.Commit();
                    res += "\n" + "serviceid=" + serviceid.ToString();

                }

                catch (Exception ex)
                {
                    Transaction1.Rollback();
                    res = ex.Message;
                    if (ex.InnerException != null) res += "\n" + ex.InnerException.Message;
                }
            }


            return res;

        }

        [HttpPut]
        public void UpdateCustomerService([FromBody] CustomerServiceModel newitem)
        {
            try
            {
                CustomerServiceModel myService = new CustomerServiceModel();

                myService = this.Auto1Context.CustomerService.Where(a => a.ServiceId == newitem.ServiceId).First();
                myService.ServiceStatus = newitem.ServiceStatus;
                myService.ServiceDateTime = DateTime.Now;
                myService.DiscountCode = newitem.DiscountCode;
                myService.DiscountAmount = newitem.DiscountAmount;
                myService.ExtraAmount = newitem.ExtraAmount;
                myService.ShippingAmount = newitem.ShippingAmount;
                myService.TotalAmount = newitem.TotalAmount;
                myService.DeliveryCode = newitem.DeliveryCode;
                myService.ServicesDesc = newitem.ServicesDesc;


                this.Auto1Context.CustomerService.Entry(myService).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.Auto1Context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPut]
        public void UpdateCustomerServiceStatus(long serviceid, byte status)
        {
            try
            {
                CustomerServiceModel myService = new CustomerServiceModel();

                myService = this.Auto1Context.CustomerService.Where(a => a.ServiceId == serviceid).First();
                myService.ServiceStatus = status;
                myService.ServiceDateTime = DateTime.Now;
                this.Auto1Context.CustomerService.Entry(myService).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.Auto1Context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPut]
        public void UpdateCustomerServicePostman(long serviceid, int postmanid)
        {
            try
            {
                CustomerServiceModel myService = new CustomerServiceModel();

                myService = this.Auto1Context.CustomerService.Where(a => a.ServiceId == serviceid).First();
                myService.PostmanId = postmanid;
                myService.ServiceDateTime = DateTime.Now;
                this.Auto1Context.CustomerService.Entry(myService).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.Auto1Context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPut]
        public void UpdateCustomerServiceItem([FromBody] CustomerServiceItemModel newitems)
        {
            try
            {
               
                this.Auto1Context.CustomerServiceItem.Entry(newitems).State =Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.Auto1Context.SaveChanges();
            }
            catch(Exception ex) 
            {

            }
        }

        [HttpPut]
        public void UpdateCustomerServiceItemQuantity(long serviceitemid, int quantity)
        {
            try
            {
                CustomerServiceItemModel myServiceitem=new CustomerServiceItemModel();

                myServiceitem=this.Auto1Context.CustomerServiceItem.Where(a => a.ServiceItemId == serviceitemid).First();
                myServiceitem.Quantity = quantity;
                this.Auto1Context.CustomerServiceItem.Entry(myServiceitem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.Auto1Context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPut]
        public void UpdateCustomerServiceItemStatus(long serviceitemid, byte status)
        {
            try
            {
                CustomerServiceItemModel myServiceitem = new CustomerServiceItemModel();

                myServiceitem = this.Auto1Context.CustomerServiceItem.Where(a => a.ServiceItemId == serviceitemid).First();
                myServiceitem.ServiceItemStatus = status;
                this.Auto1Context.CustomerServiceItem.Entry(myServiceitem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.Auto1Context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        [HttpGet(Name = "GetServiceId")]
        public long GetServiceId(long customerid = -1, int vendorid = -1)
        {
            System.FormattableString s3 = $"SELECT dbo.fn_GetServiceId( {customerid},{vendorid}) as ServiceId";
            long res = 0;

            try
            {
                res = this.Auto1Context.Set<CustomerServiceDummyModel>().FromSql(s3).First().ServiceId;
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        [HttpDelete("{serviceid}")]
        public void DeleteCustomerService(Int64 serviceid)
        {
            CustomerServiceModel customerservice = this.Auto1Context.CustomerService.Where(a => a.ServiceId == serviceid).First();
            if (customerservice != null)
            {
                List<CustomerServiceItemModel> customerserviceitems = this.Auto1Context.CustomerServiceItem.Where(a => a.ServiceId == serviceid).ToList();
                try
                {
                    this.Auto1Context.CustomerServiceItem.RemoveRange(customerserviceitems);
                    this.Auto1Context.SaveChanges();
                    this.Auto1Context.CustomerService.Remove(customerservice);
                    this.Auto1Context.SaveChanges();
                }
                catch (Exception ex)
                {
                }

            } 
        }

        [HttpDelete("{serviceitemid}")]
        public void DeleteCustomerServiceItem(Int64 serviceitemid)
        {
            try
            {
                CustomerServiceItemModel Serviceitem = this.Auto1Context.CustomerServiceItem.Where(a => a.ServiceItemId == serviceitemid).First();
                if (Serviceitem != null)
                {
                    this.Auto1Context.CustomerServiceItem.Remove(Serviceitem);
                    this.Auto1Context.SaveChanges();

                    List<CustomerServiceItemModel> newitems = this.Auto1Context.CustomerServiceItem.Where(a => a.ServiceId == Serviceitem.ServiceId).ToList();
                    if(newitems.Count == 0)
                    {
                        DeleteCustomerService(Serviceitem.ServiceId);
                    }

                }


            }
            catch (Exception ex)
            {
            }
        }



    }
}

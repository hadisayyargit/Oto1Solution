using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oto1.Models;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
//using ThreadNetwork;


namespace oto1.Services
{
    public class CustomerServiceController
    {
        private string baseUrl { get; set; }


        public CustomerServiceController()
        {
            this.baseUrl = DeviceInfo.Platform ==

            //DevicePlatform.Android ? "http://10.0.2.2:6677" : "http://localhost:6677";

            DevicePlatform.Android ? "https://khordadnet.ir/mysites/auto1api" : "https://khordadnet.ir/mysites/Auto1api";


        }

        public async Task<string> AddCustomerService(List<CustomerServiceItemViewModel> myCustomerServices)
        {
            //CustomerServiceModel res=new CustomerServiceModel();

            try
            {

                string fullurl = this.baseUrl + $"/AddCustomerService";
                HttpClient myhttpclient = new HttpClient();

                string CustomerServicesAsJason = JsonConvert.SerializeObject(myCustomerServices);
                StringContent CustomerServicesStringContent = new StringContent(CustomerServicesAsJason, Encoding.UTF8, "application/json");


                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PostAsync("", CustomerServicesStringContent);
                var contents = await myhttpresponsemessage.Content.ReadAsStringAsync();

                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    //res= await Task.FromResult<strin>(myhttpresponsemessage.Content);

                    //res = await Task.FromResult<string>(res);
                }
                
                // return await Task.FromResult<string>(res);
                return contents;


            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<List<CustomerServiceViewModel>> GetCustomerService(long serviceid = -1, long customerid = -1, int vendorid = -1, int postmanid = -1, string beginticketdatetime = "2000-1-1", string endticketdatetime = "2000-1-1", string servicestatusserie = "255")
        {

            try
            {
                List<CustomerServiceViewModel> myCustomerServices = new List<CustomerServiceViewModel>();
                string fullurl = this.baseUrl + "/GetCustomerService?serviceid="+serviceid+"&customerid=" + customerid + "&vendorid=" + vendorid + "&postmanid=" + postmanid + "&beginticketdatetime=" + beginticketdatetime + "&endticketdatetime=" + endticketdatetime + "&servicestatusserie=" + servicestatusserie;
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(60);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    myCustomerServices = JsonConvert.DeserializeObject<List<CustomerServiceViewModel>>(contentResponse);


                }

                return await Task.FromResult(myCustomerServices.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<bool> UpdateCustomerService(CustomerServiceModel myCustomerService)
        {

            try
            {
                string fullurl = this.baseUrl + $"/UpdateCustomerService";
                HttpClient myhttpclient = new HttpClient();
                string myJasonString = JsonConvert.SerializeObject(myCustomerService);
                StringContent myStringContent = new StringContent(myJasonString, Encoding.UTF8, "application/json");

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PutAsync("", myStringContent);


                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);

                }

                return await Task.FromResult(false);


            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> UpdateCustomerServiceStatus(long serviceid, byte status)
        {

            try
            {
                string fullurl = this.baseUrl + $"/UpdateCustomerServiceStatus?serviceid={serviceid}&status={status}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PutAsync("", null);

                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);

                }

                return await Task.FromResult(false);


            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> UpdateCustomerServicePostman(long serviceid, int postmanid)
        {

            try
            {
                string fullurl = this.baseUrl + $"/UpdateCustomerServicePostman?serviceid={serviceid}&postmanid={postmanid}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PutAsync("", null);

                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);

                }

                return await Task.FromResult(false);


            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> DeleteCustomerService(long serviceid)
        {

            try
            {

                string fullurl = this.baseUrl + $"/DeleteCustomerService/{serviceid}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.DeleteAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);

                }

                return await Task.FromResult(false);


            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<List<CustomerServiceItemViewModel>> GetCustomerServiceItem(long serviceid = -1, long customerid = -1, int manufacturerid = -1, int vendorid = -1, int postmanid = -1, string ticketdatetime = "2000-1-1", string servicedatetime = "2000-1-1", byte servicestatus = 255)
        {

            try
            {
                List<CustomerServiceItemViewModel> myCustomerServices = new List<CustomerServiceItemViewModel>();
                string fullurl = this.baseUrl + "/GetCustomerServiceItem?serviceid="+serviceid+"&customerid=" + customerid+ "&manufacturerid="+manufacturerid+"&vendorid="+ vendorid + "&postmanid=" + postmanid + "&ticketdatetime="+ticketdatetime+"&servicedatetime="+ servicedatetime+"&servicestatus="+ servicestatus;
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(60);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    myCustomerServices = JsonConvert.DeserializeObject<List<CustomerServiceItemViewModel>>(contentResponse);


                }

                return await Task.FromResult(myCustomerServices.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public async Task<bool> UpdateCustomerServiceItemQuantity(long serviceitemid, int quantity)
        {

            try
            {

                string fullurl = this.baseUrl + $"/UpdateCustomerServiceItemQuantity?serviceitemid={serviceitemid}&quantity={quantity}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PutAsync("", null);


                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);

                }

                return await Task.FromResult(false);


            }
            catch (Exception ex)
            {
                return false;
            }

        }



        public async Task<bool> UpdateCustomerServiceItemStatus(long serviceitemid, byte status)
        {

            try
            {
                string fullurl = this.baseUrl + $"/UpdateCustomerServiceItemStatus?serviceitemid={serviceitemid}&status={status}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PutAsync("", null);


                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);

                }

                return await Task.FromResult(false);


            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> DeleteCustomerServiceItem(long serviceitemid)
        {

            try
            {

                string fullurl = this.baseUrl + $"/DeleteCustomerServiceItem/{serviceitemid}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.DeleteAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);

                }

                return await Task.FromResult(false);


            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}

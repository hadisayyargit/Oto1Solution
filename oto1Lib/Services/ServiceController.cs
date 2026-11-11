using Newtonsoft.Json;
using oto1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace oto1.Services
{
    public class ServiceController
    {
        private string baseUrl { get; set; }

        public ServiceController()
        {
            this.baseUrl = DeviceInfo.Platform ==

            //DevicePlatform.Android ? "http://10.0.2.2:6677/Auto1" : "http://localhost:6677/auto1";

            DevicePlatform.Android ? "https://khordadnet.ir/mysites/auto1api/Auto1" : "https://khordadnet.ir/mysites/Auto1api/Auto1";


          

        }

        public async Task<string> SayHello()
        {

            try
            {
                ///https://khordadnet.ir/mysites/auto1api/Auto1/sayhello
                string fullurl = this.baseUrl + $"/sayhello";
                HttpClient myhttpclient = new HttpClient();
                string strResult = "";

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");

                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    strResult = contentResponse;


                }

                return await Task.FromResult(strResult);


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<AppUserModel> AuthenticateUser(string userid, string password)
        {

            try
            {
                AppUserModel myuser = new AppUserModel();

                string fullurl = this.baseUrl + $"/AuthenticateUser?userid={userid}&password={password}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");

                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    myuser = JsonConvert.DeserializeObject<AppUserModel>(contentResponse);


                }

                return await Task.FromResult(myuser);


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<CustomerModel> GetCustomerByUserId(string userid)
        {

            try
            {
                CustomerModel myCustomer = new CustomerModel();
                string fullurl = this.baseUrl + $"/GetCustomerByUserId?userid={userid}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");

                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    myCustomer = JsonConvert.DeserializeObject<CustomerModel>(contentResponse);


                }

                return await Task.FromResult(myCustomer);


            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<VendorModel> GetVendorByUserId(string userid)
        {

            try
            {
                VendorModel myvendor = new VendorModel();

                string fullurl = this.baseUrl + $"/GetVendorByUserId?userid={userid}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");

                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    myvendor = JsonConvert.DeserializeObject<VendorModel>(contentResponse);


                }

                return await Task.FromResult(myvendor);


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<PersonModel> GetPersonByUserId(string userid)
        {

            try
            {
                PersonModel myPerson = new PersonModel();

                string fullurl = this.baseUrl + $"/GetPersonByUserId?userid={userid}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");

                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    myPerson = JsonConvert.DeserializeObject<PersonModel>(contentResponse);


                }

                return await Task.FromResult(myPerson);


            }
            catch (Exception ex)
            {
                throw;
            }

        }
        /// /////////////////////////////////////////
        public async Task<List<CustomerModel>> GetAllCustomers()
        {


            try
            {
                List<CustomerModel> customerlist = new List<CustomerModel>();
                string fullurl = this.baseUrl + "/GetAllCustomers";
                HttpClient myhttpclient = new HttpClient();


                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(30);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    customerlist = JsonConvert.DeserializeObject<List<CustomerModel>>(contentResponse);


                }

                return await Task.FromResult(customerlist.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }
         

        public async Task<List<PersonModel>> GetAllPersons()
        {


            try
            {
                List<PersonModel> personlist = new List<PersonModel>();
                string fullurl = this.baseUrl + "/GetAllPersons";
                HttpClient myhttpclient = new HttpClient();


                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(30);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    personlist = JsonConvert.DeserializeObject<List<PersonModel>>(contentResponse);


                }

                return await Task.FromResult(personlist.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<PersonModel>> GetAllPersonsByUserRole(byte userrole)
        {
            try
            {
                List<PersonModel> personlist = new List<PersonModel>();
                string fullurl = this.baseUrl + $"/GetAllPersonsByUserRole/{userrole}";
                HttpClient myhttpclient = new HttpClient();


                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(30);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    personlist = JsonConvert.DeserializeObject<List<PersonModel>>(contentResponse);


                }

                return await Task.FromResult(personlist.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public async Task<List<BasicPrimeModel>> GetBasicPrime(string groupcode)
        {
            try
            {
                List<BasicPrimeModel> mylist = new List<BasicPrimeModel>();
                string fullurl = this.baseUrl + $"/GetBasicPrime/{groupcode}";
                HttpClient myhttpclient = new HttpClient();


                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(30);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    mylist = JsonConvert.DeserializeObject<List<BasicPrimeModel>>(contentResponse);


                }

                return await Task.FromResult(mylist.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }




        public async Task<List<CarModel>> GetAllCars()
        {

            try
            {
                List<CarModel> carlist = new List<CarModel>();
                string fullurl = this.baseUrl + "/GetAllCars";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    carlist = JsonConvert.DeserializeObject<List<CarModel>>(contentResponse);


                }

                return await Task.FromResult(carlist.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<CarModel>> GetAllCarsView()
        {

            try
            {
                List<CarModel> carlist = new List<CarModel>();
                string fullurl = this.baseUrl + "/GetAllCarsView";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    carlist = JsonConvert.DeserializeObject<List<CarModel>>(contentResponse);


                }

                return await Task.FromResult(carlist.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<List<Customer_CarModel>> GetMyCars(Int64 customerid)
        {

            try
            {
                List<Customer_CarModel> carlist = new List<Customer_CarModel>();
                string fullurl = this.baseUrl + $"/GetMyCars/{customerid}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    carlist = JsonConvert.DeserializeObject<List<Customer_CarModel>>(contentResponse);


                }

                return await Task.FromResult(carlist.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<PartModel>> GetAllParts()
        {

            try
            {
                List<PartModel> myList = new List<PartModel>();
                string fullurl = this.baseUrl + "/GetAllParts";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    myList = JsonConvert.DeserializeObject<List<PartModel>>(contentResponse);
                }

                return await Task.FromResult(myList.ToList());
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<List<PartLevelModel>> GetPartLevel(int partlevel1id = -1, int partlevel2id = -1, int partlevel3id = -1)

        // public IEnumerable<PartLevelModel> GetPartLevel(int partlevel1id = -1,int partlevel2id = -1, int partlevel3id = -1)

        {
            try
            {
                List<PartLevelModel> mylist = new List<PartLevelModel>();
                string fullurl = this.baseUrl + "/GetPartLevel?partlevel1id=" + partlevel1id + "&partlevel2id=" + partlevel2id + "&partlevel3id=" + partlevel3id ;
                HttpClient myhttpclient = new HttpClient();


                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(30);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    mylist = JsonConvert.DeserializeObject<List<PartLevelModel>>(contentResponse);


                }

                return await Task.FromResult(mylist.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }
        
        public async Task<List<PartCarVendorModel>> GetPart(int partid = -1, int manufacturerid = -1, int carid = -1, int vendorid = -1
            , int partlevel1id = -1, int partlevel2id = -1, int partlevel3id = -1, string partname = "", string partcode = ""
            , string viscosity = "", string performancelevel = "", int batteryamperage = -1, int batteryvoltage = -1, int tireyear = -1, string tireringsize = "", string tirewheels = "", string partcolor = "", string partattribute = "", int pagenumber=1, int pagesize=10)
        //, string pricedate = "2000-1-1")
        {
            try
            {
                List<PartCarVendorModel> myList = new List<PartCarVendorModel>();
                string fullurl = this.baseUrl + "/GetPart?PartId=" + partid + "&manufacturerid=" + manufacturerid + "&carid=" + carid + "&vendorid=" + vendorid
                    + "&partlevel1id=" + partlevel1id + "&partlevel2id=" + partlevel2id + "&partlevel3id=" + partlevel3id + "&partname=" + partname + "&partcode=" + partcode
                    + "&viscosity=" + viscosity + "&performancelevel=" + performancelevel + "&batteryamperage=" + batteryamperage + "&batteryvoltage=" + batteryvoltage + "&tireyear=" + tireyear + "&tireringsize=" + tireringsize + "&tirewheels=" + tirewheels + "&partcolor=" + partcolor + "&partattribute=" + partattribute
                    + "&pagenumber=" + pagenumber+ "&pagesize=" + pagesize; 
                    //+ "&pricedate=" + pricedate;

          
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    myList = JsonConvert.DeserializeObject<List<PartCarVendorModel>>(contentResponse);


                }

                return await Task.FromResult(myList.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<PartVendorViewModel>> GetVendor_Part(int vendorid)
        {

            try
            {
                List<PartVendorViewModel> mylist = new List<PartVendorViewModel>();
                string fullurl = this.baseUrl + $"/GetVendor_Part?vendorid={vendorid}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    mylist = JsonConvert.DeserializeObject<List<PartVendorViewModel>>(contentResponse);


                }

                return await Task.FromResult(mylist.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        ////////////////////////////////////////// 

        public async Task<List<LocationAddressModel>> GetAllLocationAddressess(Int64 customerid, int vendorid, bool iscustomeraddress)
        {


            try
            {
                List<LocationAddressModel> myList = new List<LocationAddressModel>();
                string fullurl = this.baseUrl + $"/GetAllLocationAddresses?customerid={customerid}&vendorid={vendorid}&iscustomeraddress=true";


                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    myList = JsonConvert.DeserializeObject<List<LocationAddressModel>>(contentResponse);


                }

                return await Task.FromResult(myList.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<LocationAddressModel> GetLocationAddress(long id)
        {

            try
            {
                LocationAddressModel myLocationAddress = new LocationAddressModel();

                string fullurl = this.baseUrl + $"/GetLocationAddress/{id}";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");

                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    myLocationAddress = JsonConvert.DeserializeObject<LocationAddressModel>(contentResponse);


                }

                return await Task.FromResult(myLocationAddress);


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<LocationAddressModel> AddLocationAddress(LocationAddressModel mylocation)
        {

            try
            {
                string fullurl = this.baseUrl + $"/AddLocationAddress";
                HttpClient myhttpclient = new HttpClient();

                string LocationAdressAsJason = JsonConvert.SerializeObject(mylocation);
                StringContent LocationAdressStringContent = new StringContent(LocationAdressAsJason, Encoding.UTF8, "application/json");

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PostAsync("", LocationAdressStringContent);
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult<LocationAddressModel>(mylocation);
                }

                return await Task.FromResult(new LocationAddressModel());


            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<LocationAddressModel> UpdateLocationAddress(LocationAddressModel mylocation)
        {

            try
            {

                string fullurl = this.baseUrl + $"/UpdateLocationAddress/{mylocation.LocationAddressId}";
                HttpClient myhttpclient = new HttpClient();

                string LocationAdressAsJason = JsonConvert.SerializeObject(mylocation);
                StringContent LocationAdressStringContent = new StringContent(LocationAdressAsJason, Encoding.UTF8, "application/json");
                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PutAsync("", LocationAdressStringContent);
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult<LocationAddressModel>(mylocation);


                }

                return await Task.FromResult(new LocationAddressModel());


            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<bool> DeleteLocationAddress(long id)
        {
            try
            {

                string fullurl = this.baseUrl + $"/DeleteLocationAddress/{id}";
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
        ////////////////////////////////////////// 


        public async Task<List<ManufacturerModel>> GetAllManufacturers()
        {

            try
            {
                List<ManufacturerModel> myList = new List<ManufacturerModel>();
                string fullurl = this.baseUrl + "/GetAllManufacturers";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    myList = JsonConvert.DeserializeObject<List<ManufacturerModel>>(contentResponse);


                }

                return await Task.FromResult(myList.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<List<VendorModel>> GetAllVendors()
        {

            try
            {
                List<VendorModel> myList = new List<VendorModel>();
                string fullurl = this.baseUrl + "/GetAllVendors";
                HttpClient myhttpclient = new HttpClient();

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.GetAsync("");
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    string contentResponse = await myhttpresponsemessage.Content.ReadAsStringAsync();

                    myList = JsonConvert.DeserializeObject<List<VendorModel>>(contentResponse);


                }

                return await Task.FromResult(myList.ToList());


            }
            catch (Exception ex)
            {
                throw;
            }

        }


        //////////////////////

        public async Task<CustomerModel> AddCustomer(CustomerModel myCustomer)
        {
            try
            {
                string fullurl = this.baseUrl + $"/AddCustomer";
                HttpClient myhttpclient = new HttpClient();

                string myJasonString = JsonConvert.SerializeObject(myCustomer);
                StringContent myStringContent = new StringContent(myJasonString, Encoding.UTF8, "application/json");

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PostAsync("", myStringContent);
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult<CustomerModel>(myCustomer);


                }

                return await Task.FromResult(new CustomerModel());


            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<VendorModel> AddVendor(VendorModel newvendor)
        {
            try
            {
                string fullurl = this.baseUrl + $"/AddVendor";
                HttpClient myhttpclient = new HttpClient();

                string myJasonString = JsonConvert.SerializeObject(newvendor);
                StringContent myStringContent = new StringContent(myJasonString, Encoding.UTF8, "application/json");

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PostAsync("", myStringContent);
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult<VendorModel>(newvendor);
                }

                return await Task.FromResult(new VendorModel());


            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<Vendor_PartModel> AddVendorPart(Vendor_PartModel newvendorpart)
        {
            try
            {
                string fullurl = this.baseUrl + $"/AddVendorPart";
                HttpClient myhttpclient = new HttpClient();

                string myJasonString = JsonConvert.SerializeObject(newvendorpart);
                StringContent myStringContent = new StringContent(myJasonString, Encoding.UTF8, "application/json");

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PostAsync("", myStringContent);
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult<Vendor_PartModel>(newvendorpart);
                }

  
                return await Task.FromResult(new Vendor_PartModel());


            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }

        }

        public async Task<PersonModel> AddPerson(PersonModel newperson)
        {
            try
            {
                string fullurl = this.baseUrl + $"/AddPerson";
                HttpClient myhttpclient = new HttpClient();

                string myJasonString = JsonConvert.SerializeObject(newperson);
                StringContent myStringContent = new StringContent(myJasonString, Encoding.UTF8, "application/json");

                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PostAsync("", myStringContent);
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult<PersonModel>(newperson);
                }

                return await Task.FromResult(new PersonModel());


            }
            catch (Exception ex)
            {
                return null;
            }

        }
        ///////////////////////
        public async Task<AppUserModel> AddUser(AppUserModel myuser)
        {

            try
            {

                string fullurl = this.baseUrl + $"/AddUser";
                HttpClient myhttpclient = new HttpClient();

                string myJasonString = JsonConvert.SerializeObject(myuser);
                StringContent myStringContent = new StringContent(myJasonString, Encoding.UTF8, "application/json");


                myhttpclient.BaseAddress = new Uri(fullurl);
                myhttpclient.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage myhttpresponsemessage = await myhttpclient.PostAsync("", myStringContent);
                if (myhttpresponsemessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult<AppUserModel>(myuser);


                }

                return await Task.FromResult(new AppUserModel());


            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<string> UploadImageAsync(byte[] imageBytes, string subFolder)
        {

            string baseUrl = this.baseUrl.TrimEnd('/');
            
            string uploadUrl = $"{baseUrl}/api/User/upload/{subFolder}";


            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(5);

                using (var content = new MultipartFormDataContent())
                {
                    var byteContent = new ByteArrayContent(imageBytes);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                    // نام فیلد "file" باید دقیقاً با نام ورودی متد در API یکی باشد
                    content.Add(byteContent, "file", $"img_{DateTime.Now.Ticks}.jpg");

                    try
                    {
                        var response = await client.PostAsync(uploadUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string relativePath = await response.Content.ReadAsStringAsync();
                            return relativePath;
                        }
                        else
                        {
                            string errorDetail = await response.Content.ReadAsStringAsync();
                            return $"Error: {response.StatusCode} - {errorDetail}";
                        }
                    }
                    catch (Exception ex)
                    {
                        return $"Exception: {ex.Message}";
                    }
                }
            }
        }


        /*
        public async Task<string> UploadBytesAsync(byte[] imageBytes, string imageURL)
        {
            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    // استفاده از ByteArrayContent به جای StreamContent که احتمال باگ استریم را صفر می‌کند
                    var byteContent = new ByteArrayContent(imageBytes);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                    content.Add(byteContent, "file", "p100.jpg");

                    try
                    {
                        var response = await client.PostAsync(imageURL, content);

                        if (response.IsSuccessStatusCode)
                        {
                            return "1";
                        }
                        else
                        {
                            string errorContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine($"Error Code: {response.StatusCode}, Message: {errorContent}");
                            return $"0 - Error: {response.StatusCode}";
                        }
                    }
                    catch (Exception ex)
                    {
                        return $"0 - Exception: {ex.Message}";
                    }
                }
            }
        }
        */

        /*
        public async Task<string> UploadImageAsync(Stream imageStream, string imageURL)
        {
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                var streamContent = new StreamContent(imageStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                content.Add(streamContent, "file", "c22.jpg");

                try
                {
                    var response = await client.PostAsync(imageURL, content);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        return "1";

                    }
                    else
                    {
                        return "0";
                    }
                }
                catch (Exception ex)
                {
                    return "0";
                }
            }
        }
        */

        /*

        public async Task<string> UploadImageAsync(byte[] imageBytes, string uploadUrl, string fileName)
        {
            using var client = new HttpClient();
            using var content = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(imageBytes);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            content.Add(fileContent, "file", fileName);
            var response = await client.PostAsync(uploadUrl, content);
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                return $"{(int)response.StatusCode} {body}";
            return "1";
        }


        public async Task<bool> UpdateCustomer(CustomerModel myCustomer)
        {

            try
            {
                string fullurl = this.baseUrl + $"/UpdateCustomer";
                HttpClient myhttpclient = new HttpClient();
                string myJasonString = JsonConvert.SerializeObject(myCustomer);
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

        */

        public async Task<bool> UpdateVendor(VendorModel myVendor)
        {

            try
            {
                string fullurl = this.baseUrl + $"/UpdateVendor";
                HttpClient myhttpclient = new HttpClient();
                string myJasonString = JsonConvert.SerializeObject(myVendor);
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

        public async Task<bool> UpdateVendorPart(Vendor_PartModel newvendorpart)
        {

            try
            {
                string fullurl = this.baseUrl + $"/UpdateVendorPart";
                HttpClient myhttpclient = new HttpClient();
                string myJasonString = JsonConvert.SerializeObject(newvendorpart);
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


        public async Task<bool> UpdateVendorPartExistance(int vendorid, int partid, int addedquantity)
        {

            try
            {

                string fullurl = this.baseUrl + $"/UpdateVendorPartExistance?vendorid={vendorid}&partid={partid}&addedquantity={addedquantity}";
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
        public async Task<bool> UpdatePerson(PersonModel myPerson)
        {

            try
            {
                string fullurl = this.baseUrl + $"/UpdatePerson";
                HttpClient myhttpclient = new HttpClient();
                string myJasonString = JsonConvert.SerializeObject(myPerson);
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
        public async Task<bool> UpdateUserPassword(string userid, string oldpassword, string newpassword)
        {

            string fullurl = this.baseUrl + $"/UpdateUserPassword?userid={userid}&oldpassword={oldpassword}&newpassword={newpassword}";
            try
            {
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
                throw;
            }
        }
    }
    /*
private async Task SaveImageToDatabase(byte[] imageBytes)
{
var connectionString = "Your SQL Server connection string here";
using (var connection = new SqlConnection(connectionString))
{
await connection.OpenAsync();
var query = "INSERT INTO YourTableName (ImageColumn) VALUES (@Image)";
using (var command = new SqlCommand(query, connection))
{
command.Parameters.AddWithValue("@Image", imageBytes);
await command.ExecuteNonQueryAsync();
}
}
}

*/


    
}
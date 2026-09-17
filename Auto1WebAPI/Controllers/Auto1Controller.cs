using Auto1API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Auto1API.Controllers
{    
    [ApiController]
    [Route("[controller]/[action]")]
    public class Auto1Controller : ControllerBase
    {
        public Auto1Context Auto1Context { get; set; }
        public Auto1Controller(Auto1Context _auto1context)
        {
            this.Auto1Context = _auto1context;


        }

        [HttpGet]
        public string sayhello()
        {
            string s = "Hi1";

            return s;
        }

        // GET: <Auto1Controller>
        [HttpGet(Name = "GetAllCustomers")]
        public IEnumerable<CustomerModel> GetAllCustomers()
        {
            return this.Auto1Context.Customer.ToList();
        }


        [HttpGet(Name = "AuthenticateUser")]
        public AppUserModel AuthenticateUser(string userid, string password)
        {
            try
            {
                return this.Auto1Context.AppUser.Where(p => p.UserId == userid && p.Password == password).First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet(Name = "GetCustomerByUserId")]
        public CustomerModel GetCustomerByUserId(string userid)
        {
            // myCup = db.Cup.Where(c => c.IsDefaultCup == true).FirstOrDefault();
            try
            {
                return this.Auto1Context.Customer.Where(p => p.UserId == userid ).First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        [HttpGet(Name = "GetVendorByUserId")]
        public VendorModel GetVendorByUserId(string userid)
        {
            try
            {
                return this.Auto1Context.Vendor.Where(p => p.UserId == userid).First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet(Name = "GetPersonByUserId")]
        public PersonModel GetPersonByUserId(string userid)
        {
            try
            {
                return this.Auto1Context.Person.Where(p => p.UserId == userid).First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet(Name = "GetAllCars")]
        public IEnumerable<CarModel> GetAllCars()
        {
           
            return this.Auto1Context.Car.ToList();
        }

        [HttpGet(Name = "GetAllCarsView")]
        public IEnumerable<CarModel> GetAllCarsView()
        {
            //get selected columns
            var ss = this.Auto1Context.Car.Select(s => new CarModel
            {
                CarId = s.CarId,
                FName = s.FName,
                LName = s.LName
            }).ToList();

            return ss;
        }

        [HttpGet("{customerid}")]
        public IEnumerable<Customer_CarModel> GetMyCars(long customerid)
        {
            List<Customer_CarModel> mycars = new List<Customer_CarModel>();


            mycars = (from m in this.Auto1Context.Customer_Car
                      join t1 in this.Auto1Context.Customer on m.CustomerId equals t1.CustomerId into g1
                      from mt1 in g1.DefaultIfEmpty()
                      join t2 in this.Auto1Context.Car on m.CarId equals t2.CarId into g2
                      from mt2 in g2.DefaultIfEmpty()
                      select new Customer_CarModel
                      {
                          CustomerId = m.CustomerId
                          ,
                          CarId = m.CarId
                          ,
                          FName = mt2.FName
                          ,
                          Plaque = m.Plaque
                          //,  ThumbnailPhoto = mt2.ThumbnailPhotoFile

                      }).Where(m => m.CustomerId == customerid).ToList();


            /*
            mycars=myCmsList.Join(db.AppUser, c => c.AuthorId, a => a.UserId, (c, a) => new CmsView { CmsId = c.CmsId, CupId = c.CupId, Title = c.Title, Body = c.Body, AuthorId = c.AuthorId, IsActive = c.IsActive, IsChat = c.IsChat, IsSlider = c.IsSlider, ReadStatus = c.ReadStatus, ParentCmsId = c.ParentCmsId, IsHotNews = c.IsHotNews, PublishDate = c.PublishDate, PictureFile = c.PictureFile, AuthorDs = a.UserDs, AuthorThumbnail = a.ThumbnailPhoto }).ToList();

            myPredictionList = db.Database.SqlQuery<PredictionView>("spGetPrediction @cupid = {0}, @matchstate=0", new object[] { ((Cup)Session[GlobalModule.m_CurrentCup]).CupId }).OrderBy(m => m.MatchTime).OrderBy(m => m.MatchTime).ToList();
                           

            */

            return mycars;
        }

        /////////////////////////////////////////////////////////////////////

        /// <Product>


        [HttpGet(Name = "GetAllParts")]
        public IEnumerable<PartModel> GetAllParts()
        {

            return this.Auto1Context.Part.ToList();
        }

        [HttpGet(Name = "GetPart")]
        public IEnumerable<PartCarVendorModel> GetPart(int partid=-1, int manufacturerid = -1, string viscosity="",string performancelevel = "", int carid=-1, int vendorid=-1, string pricedate="2000-1-1")
        {
            //string s1= string.Format("sp_GetPart @PartId = {0}, @manufacturerid={1}, @viscosity={2},@performancelevel={3},@carid={4}",PartId,manufacturerid,viscosity,performancelevel,carid);
            //string s2= $"sp_GetPart @PartId = {PartId}, @manufacturerid={manufacturerid}, @viscosity={viscosity},@performancelevel={performancelevel},@carid={carid}";
            //System.FormattableString s3 = $"sp_GetPart @PartId ={PartId}, @manufacturerid={manufacturerid}, @viscosity='{viscosity}',@performancelevel='{performancelevel}',@carid={carid} ,@vendorid={vendorid} , @pricedate='{pricedate}'";
            System.FormattableString s3 = $"sp_GetPart @partid ={partid}, @manufacturerid={manufacturerid}, @viscosity={viscosity},@performancelevel={performancelevel},@carid={carid} ,@vendorid={vendorid} , @pricedate={pricedate}";

           // System.FormattableString s3 = $"sp_SearchProducts @PartId =1,@carid=1";

            List<PartCarVendorModel> res = this.Auto1Context.Set<PartCarVendorModel>().FromSql(s3).ToList();
           
            ///doesnt work
            //return this.Auto1Context.Database.SqlQuery<ProductCarVendorModel>(s3).OrderBy(m => m.PartId).OrderBy(m => m.CarId).ToList();

            return res;
            
        }


        [HttpGet(Name = "GetVendor_Part")]
        public IEnumerable<PartVendorModel> GetVendor_Part(int vendorid = -1)
        {
            // System.FormattableString s3 = $"select vp.Id,vp.PartId, vp.VendorId,vp.ValidBeginDate,vp.ValidEndDate,vp.PriceAmount,vp.DiscountPercent,vp.Existance, p.FName, p.LName, FORMAT(vp.ValidBeginDate,'yyyy/MM/dd','fa-IR') as jalaliBeginDate, FORMAT(vp.ValidEndDate,'yyyy/MM/dd','fa-IR') as jalaliEndDate from Vendor_Part vp inner join Part p on p.PartId=vp.PartId where vp.VendorId={vendorid}";

            System.FormattableString s3 = $"select vp.Id,vp.PartId, vp.VendorId,vp.ValidBeginDate,vp.ValidEndDate,vp.PriceAmount,vp.DiscountPercent,vp.Existance, p.FName, p.LName, FORMAT(vp.ValidBeginDate,'yyyy/MM/dd','fa-IR') as jalaliBeginDate, FORMAT(vp.ValidEndDate,'yyyy/MM/dd','fa-IR') as jalaliEndDate from Vendor_Part vp inner join Part p on p.PartId=vp.PartId where vp.VendorId={vendorid}";

            List<PartVendorModel> res = new List<PartVendorModel>();

            try
            {
                res = this.Auto1Context.Set<PartVendorModel>().FromSql(s3).ToList();
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        /////////////////////////////////////////////////////////////////////


        [HttpGet(Name = "GetAllLocationAddresses")]
        public IEnumerable<LocationAddressModel> GetAllLocationAddresses(Int64 customerid, int vendorid, bool iscustomeraddress)
        {
            
            if (iscustomeraddress)
            {
                return this.Auto1Context.LocationAddress.Where(a=>a.CustomerId==customerid).ToList();

            }
            else
            {
                return this.Auto1Context.LocationAddress.Where(a => a.VendorId == vendorid).ToList();
            }
        }

        [HttpGet("{id}")]
        public LocationAddressModel GetLocationAddress(long id)
        {            
            
            return this.Auto1Context.LocationAddress.Where(p => p.LocationAddressId == id).First();
        }

        // POST api/<Auto1Controller>
        [HttpPost]
        public void AddLocationAddress([FromBody] LocationAddressModel newlocation)
        {
            this.Auto1Context.LocationAddress.Add(newlocation);
            this.Auto1Context.SaveChanges();
        }

        
        // PUT api/<Auto1Controller>/5
        [HttpPut("{id}")]
        public void UpdateLocationAddress([FromBody] LocationAddressModel newlocation)
        {
            LocationAddressModel mylocation=this.GetLocationAddress(newlocation.LocationAddressId);
            if (mylocation!=null)
            {
                mylocation.FName=newlocation.FName;
                mylocation.Address = newlocation.Address;
                mylocation.VendorId=newlocation.VendorId;
                mylocation.CustomerId=newlocation.CustomerId;
                mylocation.Latitude=newlocation.Latitude;
                mylocation.Longitude=newlocation.Longitude;

                this.Auto1Context.LocationAddress.Entry(mylocation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.Auto1Context.SaveChanges();
            }
        }

        // DELETE api/<Auto1Controller>/5
        [HttpDelete("{id}")]
        public void DeleteLocationAddress(Int64 id)
        {
            LocationAddressModel mylocation = this.GetLocationAddress(id);
            if (mylocation != null)
            {
                this.Auto1Context.LocationAddress.Remove(mylocation);
                this.Auto1Context.SaveChanges();
            }
        }

        
        /////////////////////////////////////////////////////////////////////

        [HttpGet(Name = "GetAllManufacturers")]
        public IEnumerable<ManufacturerModel> GetAllManufacturers()
        {

            return this.Auto1Context.Manufacturer.ToList();
        }

        [HttpGet(Name = "GetAllVendors")]
        public IEnumerable<VendorModel> GetAllVendors()
        {

            return this.Auto1Context.Vendor.ToList();
        }

        [HttpGet(Name = "GetAllPersons")]
        public IEnumerable<PersonModel> GetAllPersons()
        {

            return this.Auto1Context.Person.ToList();
        }

        ////////////////////////////
        ///
        [HttpPost]
        public void AddUser([FromBody] AppUserModel newuser)
        {
            this.Auto1Context.AppUser.Add(newuser);
            this.Auto1Context.SaveChanges();
        }

        [HttpPost]
        public void AddCustomer([FromBody] CustomerModel newcustomer)
        {
            this.Auto1Context.Customer.Add(newcustomer);
            this.Auto1Context.SaveChanges();
        }

        [HttpPost]
        public void AddVendor([FromBody] VendorModel newvendor)
        {
            this.Auto1Context.Vendor.Add(newvendor);
            this.Auto1Context.SaveChanges();
        }
        [HttpPost]
        public void AddPerson([FromBody] PersonModel newperson)
        {
            this.Auto1Context.Person.Add(newperson);
            this.Auto1Context.SaveChanges();
        }

        [HttpPut]
        public void UpdateCustomer([FromBody] CustomerModel mycustomer)
        {
            this.Auto1Context.Customer.Entry(mycustomer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.Auto1Context.SaveChanges();
        }

        [HttpPut]
        public void UpdateVendor([FromBody] VendorModel myvendor)
        {
            this.Auto1Context.Vendor.Entry(myvendor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.Auto1Context.SaveChanges();
        }

        [HttpPut]
        public void UpdatePerson([FromBody] PersonModel myperson)
        {
            this.Auto1Context.Person.Entry(myperson).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.Auto1Context.SaveChanges();
        }

        [HttpPut]
        public void UpdateUserPassword(string userid, string oldpassword, string newpassword)
        {
            AppUserModel user1 = new AppUserModel();
            try
            {
                user1 = this.Auto1Context.AppUser.Where(p => p.UserId == userid && p.Password == oldpassword).First();
                if (user1 != null)
                {
                    user1.Password = newpassword;
                    this.Auto1Context.AppUser.Entry(user1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    this.Auto1Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPut]
        public AppUserModel ActivateUser(string userid, bool isactive)
        {
            AppUserModel user1 = new AppUserModel();
            try
            {
                user1 = this.Auto1Context.AppUser.Where(p => p.UserId == userid).First();
                if (user1 != null)
                {
                    user1.IsActive = isactive;
                    this.Auto1Context.AppUser.Entry(user1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    this.Auto1Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                user1 = null;
            }

            return user1;
        }

  

    }
}

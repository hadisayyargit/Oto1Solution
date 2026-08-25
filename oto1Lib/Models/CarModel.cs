
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oto1.Models
{
    public class CarModel
    {
        [Key]
        public int CarId { get; set; }
        [MaxLength(50)]
        public required string FName { get; set; }

        [MaxLength(50)]
        public string LName { get; set; }
        public string CarGroup { get; set; }
        public int? CarGroupId { get; set; }
        public string Brand { get; set; }
        public string Company { get; set; }

        public int? VehicleType { get; set; }

        public string ThumbnailPhotoFile { get; set; }
        private string baseUrl { get; set; }

        /*
        public async Task<List<CarModel>> GetAllCars()
        {

            this.baseUrl = DeviceInfo.Platform ==

            //DevicePlatform.Android ? "http://10.0.2.2:6677/Auto1" : "http://localhost:6677/auto1";

            DevicePlatform.Android ? "https://khordadnet.ir/mysites/auto1api/Auto1" : "https://khordadnet.ir/mysites/Auto1api/Auto1";


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
        */
    }
}

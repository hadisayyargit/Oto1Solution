using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oto1.Models;

namespace oto1
{
    public partial class GlobalClass
    {
        public enum enumServiceType:byte
        {
            servicetype_Part=0, servicetype_CarTowing = 4, servicetype_OnLocationService = 2, servicetype_OnSiteService = 3, servicetype_SpecialService=5
                , servicetype_NoSelect=255
        }

        public enum enumServiceItemType : byte
        {         
          serviceitemtype_LubricantSale = 11, serviceitemtype_PartSale = 12, serviceitemtype_BatterySale = 13, serviceitemtype_tiresale = 14
          , serviceitemtype_tirepuncture =21, serviceitemtype_timingbelt = 22, serviceitemtype_Fuel = 23
          , serviceitemtype_carwire = 24, serviceitemtype_lock = 25, serviceitemtype_customerplaceservice = 26
          , serviceitemtype_LubricantService = 31, serviceitemtype_carwash = 32, serviceitemtype_troubleshooting = 33, serviceitemtype_carwindow = 33
          , serviceitemtype_towing_light = 41, serviceitemtype_towing_Heavy = 42
          , serviceitemtype_NoSelect = 255
        }

        public enum enumServiceStatus : byte
        {
            servicestatus_Ticketing = 0, servicestatus_Payment = 1, servicestatus_Pending=2, servicestatus_Refusal = 3, servicestatus_Sending = 4, servicestatus_Done = 5
                , servicestatus_Imperfect = 6, servicestatus_Reject = 7
                , servicestatus_NoSelect = 255
        }

        public enum enumServiceItemStatus : byte
        {
            serviceitemstatus_Request = 0, serviceitemstatus_Done = 1, serviceitemstatus_Noexist = 2, serviceitemstatus_Reject = 3, serviceitemstatus_NoSelect = 255
        }
        public enum enumUserRole : byte
        {
            userrole_admin = 5, userrole_customer = 1, userrole_vendor = 2, userrole_postman = 3, userrole_NoSelect = 255
        }

        public static decimal? m_Latitude;
        public static decimal? m_Longitude;
        public static string m_Address;
        public static bool m_MapSelected;
        public static string m_UserId;
        public static string m_VendorName;
        public static int m_VendorId;
        public static long m_CustomerId;
        public static string m_CustomerName;
        public static int m_PersonId;
        public static string m_PersonName;

        public static CustomerServiceItemViewModel m_CustomerServiceItemView;
    }
}

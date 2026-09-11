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
            servicetype_Part=0, servicetype_CarTowing = 1, servicetype_OnLocationService = 2, servicetype_OnSiteService = 3, servicetype_SpecialService=4
                , servicetype_NoSelect=255
        }

        public enum enumServiceItemType : byte
        {
            serviceitemtype_PartSale=0,serviceitemtype_LubricantService = 1, serviceitemtype_Battery = 2, serviceitemtype_Fuel =3, serviceitemtype_tirepuncture = 4, serviceitemtype_timingbelt = 5, serviceitemtype_carwash = 6, serviceitemtype_carwire = 7, serviceitemtype_troubleshooting = 8, serviceitemtype_towing_light = 9, serviceitemtype_towing_Heavy = 10, serviceitemtype_DoorOpening=11,  serviceitemtype_NoSelect = 255
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

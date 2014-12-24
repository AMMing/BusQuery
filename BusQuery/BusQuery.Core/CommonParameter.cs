using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public class CommonParameter
    {
        public static bool AddressStoryboardIsOut = false;
        public static string BLNearBus;
        public static string BLNextBus;
        public static string BusLockStationString;
        public static BusLineQueryParam ComBusLineQueryParam;
        public static string DDBeginOrEnd;
        public static string FeedBackStr;
        public static bool isAppointmentSave = false;
        public static bool IsAppointmentXLSearch = false;
        public static bool isLogin = false;
        public static bool isUseMapCollection = false;
        public static string PhoneNumber = "";
        public static string sessionid;
        public static string StationType = "";
        public static string WebUri = "";
        public static BusLineQueryParam XLBusLineQueryParam;
        public static BusLineQueryResultVer2 XLBusLineQueryResultVer2;
        public static BusLineVer2 XLBusLineVer2;
        public static double ZoomLevel;
    }
}

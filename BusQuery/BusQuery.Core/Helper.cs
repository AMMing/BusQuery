using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public class Helper
    {
        public void GetLuXian(string txt, Action<bool, List<BusLineVer2>> callback)
        {
            BusLineQueryParam param = new BusLineQueryParam();
            param.setRouteNumber(txt);
            param.setCityName("厦门市");

            HttpProcessor httpProcessor = new HttpProcessor(0x19, param);
            httpProcessor.RequestCompleted += bytes =>
            {
                if (bytes != null)
                {
                    BusLineQueryResultVer2 XLBusLineQueryResultVer2 = new BusLineQueryResultVer2();
                    XLBusLineQueryResultVer2 = BusLineQueryResultVer2.deserialize(bytes);

                    callback(true, XLBusLineQueryResultVer2.getLines());
                    return;
                }
                callback(false, null);
            };
            httpProcessor.RequestFaild += () =>
            {
                callback(false, null);
            };

            httpProcessor.BeginRequest();

        }


        public void GetStationList(BusLineVer2 ver2, Action<bool, List<BusLineStationVer2>> callback)
        {
            BusLineQueryParam param = new BusLineQueryParam();
            param.setCityName("厦门市");
            param.setRouteNumber(ver2.BusName);
            param.setUpperOrDown(ver2.UpperOrDown);

            HttpProcessor httpProcessor = new HttpProcessor(0x1a, param);
            httpProcessor.RequestCompleted += bytes =>
            {
                if (bytes != null)
                {
                    BusLineStationsVer2 busLineStationsVer2 = new BusLineStationsVer2();
                    busLineStationsVer2 = BusLineStationsVer2.deserialize(bytes);

                    callback(true, busLineStationsVer2.getStations());
                    return;
                }
                callback(false, null);
            };
            httpProcessor.RequestFaild += () =>
            {
                callback(false, null);
            };

            httpProcessor.BeginRequest();

        }




        public void GetBusNow(BusLineVer2 ver2, Action<bool, List<RouteRTimeInfo>> callback)
        {
            BusLineDirection direction = new BusLineDirection
            {
                routeNumber = ver2.BusName,
                upperOrDown = ver2.UpperOrDown,
                readAllStation = false
            };
            List<BusLineDirection> busLineDirectionVector = new List<BusLineDirection> {
                direction
            };
            BusLineDirectionVector sendContent = new BusLineDirectionVector();
            sendContent.set_BusLineDirectionVector(busLineDirectionVector);

            HttpProcessor httpProcessor = new HttpProcessor(0x91, sendContent);
            httpProcessor.RequestCompleted += bytes =>
            {
                if (bytes != null)
                {
                    RouteRTimeInfoVector vector = RouteRTimeInfoVector.deserialize(bytes);

                    callback(true, vector.getRouteRTimeInfoVectorVector());
                    return;
                }
                callback(false, null);
            };
            httpProcessor.RequestFaild += () =>
            {
                callback(false, null);
            };

            httpProcessor.BeginRequest();
        }






    }
}

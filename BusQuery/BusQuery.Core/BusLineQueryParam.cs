using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{

    public class BusLineQueryParam : ISendContent
    {
        private string cityName = "厦门市";
        private bool isFirst;
        private string routeNumber = "";
        private string stationName = "";
        private string stationOrder = "";
        private string upperOrDown = "";

        public static BusLineQueryParam deserialize(byte[] byteArray)
        {
            BusLineQueryParam param = new BusLineQueryParam();
            BinaryStream stream = new BinaryStream(byteArray);
            try
            {
                param.setCityName(stream.readUTF());
                param.setRouteNumber(stream.readUTF());
                param.setUpperOrDown(stream.readUTF());
                param.setStationName(stream.readUTF());
                param.setStationOrder(stream.readUTF());
                param.setIsFirst(stream.readBoolean());
                stream.close();
            }
            catch (Exception)
            {
            }
            return param;
        }

        public string getCityName()
        {
            return this.cityName;
        }

        public bool getIsFirst()
        {
            return this.isFirst;
        }

        public string getRouteNumber()
        {
            return this.routeNumber;
        }

        public string getStationName()
        {
            return this.stationName;
        }

        public string getStationOrder()
        {
            return this.stationOrder;
        }

        public string getUpperOrDown()
        {
            return this.upperOrDown;
        }

        public byte[] serialize()
        {
            BinaryStream stream = new BinaryStream();
            try
            {
                stream.writeUTF(this.cityName);
                stream.writeUTF(this.routeNumber);
                stream.writeUTF(this.upperOrDown);
                stream.writeUTF(this.stationName);
                stream.writeUTF(this.stationOrder);
                stream.writeBoolean(this.isFirst);
                stream.close();
            }
            catch (Exception)
            {
            }
            return stream.toBytes();
        }

        public void setCityName(string cityName)
        {
            this.cityName = cityName;
        }

        public void setIsFirst(bool isFirst)
        {
            this.isFirst = isFirst;
        }

        public void setRouteNumber(string routeNumber)
        {
            this.routeNumber = routeNumber;
        }

        public void setStationName(string stationName)
        {
            this.stationName = stationName;
        }

        public void setStationOrder(string stationOrder)
        {
            this.stationOrder = stationOrder;
        }

        public void setUpperOrDown(string upperOrDown)
        {
            this.upperOrDown = upperOrDown;
        }
    }
}

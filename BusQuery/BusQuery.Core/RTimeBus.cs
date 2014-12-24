using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public class RTimeBus : ISendContent
    {
        private double bus_lat;
        private double bus_lng;
        private string busNumber = "";
        private string crowdedStatus = "";
        private int index = 0x3e7;
        private double station_lat;
        private double station_lng;
        private string stationName = "";
        private string statusType = "";

        public static RTimeBus deserialize(byte[] data)
        {
            RTimeBus bus = new RTimeBus();
            try
            {
                BinaryStream stream = new BinaryStream(data);
                bus.stationName = stream.readUTF();
                bus.statusType = stream.readUTF();
                bus.index = stream.readInt();
                bus.station_lat = stream.readDouble();
                bus.station_lng = stream.readDouble();
                bus.bus_lat = stream.readDouble();
                bus.bus_lng = stream.readDouble();
                bus.busNumber = stream.readUTF();
                bus.crowdedStatus = stream.readUTF();
                stream.close();
            }
            catch (Exception)
            {
            }
            return bus;
        }

        public double getBus_lat()
        {
            return this.bus_lat;
        }

        public double getBus_lng()
        {
            return this.bus_lng;
        }

        public string getBusNumber()
        {
            return this.busNumber;
        }

        public string getCrowdedStatus()
        {
            return this.crowdedStatus;
        }

        public int getIndex()
        {
            return this.index;
        }

        public double getStation_lat()
        {
            return this.station_lat;
        }

        public double getStation_lng()
        {
            return this.station_lng;
        }

        public string getStationName()
        {
            return this.stationName;
        }

        public string getStatusType()
        {
            return this.statusType;
        }

        public byte[] serialize()
        {
            BinaryStream stream = new BinaryStream();
            try
            {
                stream.writeUTF(this.stationName);
                stream.writeUTF(this.statusType);
                stream.writeInt(this.index);
                stream.writeDouble(this.station_lat);
                stream.writeDouble(this.station_lng);
                stream.writeDouble(this.bus_lat);
                stream.writeDouble(this.bus_lng);
                stream.writeUTF(this.busNumber);
                stream.writeUTF(this.crowdedStatus);
                stream.close();
            }
            catch (Exception)
            {
            }
            return stream.toBytes();
        }

        public void setBus_lat(double bus_lat)
        {
            this.bus_lat = bus_lat;
        }

        public void setBus_lng(double bus_lng)
        {
            this.bus_lng = bus_lng;
        }

        public void setBusNumber(string busNumber)
        {
            this.busNumber = busNumber;
        }

        public void setCrowdedStatus(string crowdedStatus)
        {
            this.crowdedStatus = crowdedStatus;
        }

        public void setIndex(int index)
        {
            this.index = index;
        }

        public void setStation_lat(double station_lat)
        {
            this.station_lat = station_lat;
        }

        public void setStation_lng(double station_lng)
        {
            this.station_lng = station_lng;
        }

        public void setStationName(string stationName)
        {
            this.stationName = stationName;
        }

        public void setStatusType(string statusType)
        {
            this.statusType = statusType;
        }
    }
}

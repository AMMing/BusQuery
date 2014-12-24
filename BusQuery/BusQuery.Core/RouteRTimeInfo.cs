using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public class RouteRTimeInfo : ISendContent
    {
        private string allStations = "";
        private string allStationsLat = "";
        private string allStationsLng = "";
        private string beginTime = "";
        private string comments = "";
        private string endTime = "";
        private string planTime = "";
        private string routeName = "";
        private RTimeBusVector rTimeBusVector = new RTimeBusVector();
        private string station_lat = "";
        private string station_lng = "";
        private string stationIndex = "";
        private string stationName = "";
        private string upperOrDown = "";

        public static RouteRTimeInfo deserialize(byte[] data)
        {
            RouteRTimeInfo info = new RouteRTimeInfo();
            try
            {
                BinaryStream stream = new BinaryStream(data);
                info.routeName = stream.readUTF();
                info.upperOrDown = stream.readUTF();
                info.stationIndex = stream.readUTF();
                info.stationName = stream.readUTF();
                info.beginTime = stream.readUTF();
                info.endTime = stream.readUTF();
                info.planTime = stream.readUTF();
                int count = stream.readInt();
                byte[] buffer = new byte[count];
                buffer = stream.readBytes(count);
                info.setrTimeBusVector(RTimeBusVector.deserialize(buffer));
                info.station_lat = stream.readUTF();
                info.station_lng = stream.readUTF();
                info.allStations = stream.readUTF();
                info.comments = stream.readUTF();
                try
                {
                    info.allStationsLat = stream.readUTF();
                    info.allStationsLng = stream.readUTF();
                }
                catch (Exception)
                {
                }
                stream.close();
            }
            catch (Exception)
            {
            }
            return info;
        }

        public string getAllStations()
        {
            return this.allStations;
        }

        public string getAllStationsLat()
        {
            return this.allStationsLat;
        }

        public string getAllStationsLng()
        {
            return this.allStationsLng;
        }

        public string getBeginTime()
        {
            return this.beginTime;
        }

        public string getComments()
        {
            return this.comments;
        }

        public string getEndTime()
        {
            return this.endTime;
        }

        public string getPlanTime()
        {
            return this.planTime;
        }

        public string getRouteName()
        {
            return this.routeName;
        }

        public RTimeBusVector getrTimeBusVector()
        {
            return this.rTimeBusVector;
        }

        public string getStation_lat()
        {
            return this.station_lat;
        }

        public string getStation_lng()
        {
            return this.station_lng;
        }

        public string getStationIndex()
        {
            return this.stationIndex;
        }

        public string getStationName()
        {
            return this.stationName;
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
                stream.writeUTF(this.routeName);
                stream.writeUTF(this.upperOrDown);
                stream.writeUTF(this.stationIndex);
                stream.writeUTF(this.stationName);
                stream.writeUTF(this.beginTime);
                stream.writeUTF(this.endTime);
                stream.writeUTF(this.planTime);
                byte[] buffer = this.rTimeBusVector.serialize();
                stream.writeInt(buffer.Length);
                stream.writeBytes(buffer);
                stream.writeUTF(this.station_lat);
                stream.writeUTF(this.station_lng);
                stream.writeUTF(this.allStations);
                stream.writeUTF(this.comments);
                stream.writeUTF(this.allStationsLat);
                stream.writeUTF(this.allStationsLng);
                stream.close();
            }
            catch (Exception)
            {
            }
            return stream.toBytes();
        }

        public void setAllStations(string allStations)
        {
            this.allStations = allStations;
        }

        public void setAllStationsLat(string allStationsLat)
        {
            this.allStationsLat = allStationsLat;
        }

        public void setAllStationsLng(string allStationsLng)
        {
            this.allStationsLng = allStationsLng;
        }

        public void setBeginTime(string beginTime)
        {
            this.beginTime = beginTime;
        }

        public void setComments(string comments)
        {
            this.comments = comments;
        }

        public void setEndTime(string endTime)
        {
            this.endTime = endTime;
        }

        public void setPlanTime(string planTime)
        {
            this.planTime = planTime;
        }

        public void setRouteName(string routeName)
        {
            this.routeName = routeName;
        }

        public void setrTimeBusVector(RTimeBusVector rTimeBusVector)
        {
            this.rTimeBusVector = rTimeBusVector;
        }

        public void setStation_lat(string station_lat)
        {
            this.station_lat = station_lat;
        }

        public void setStation_lng(string station_lng)
        {
            this.station_lng = station_lng;
        }

        public void setStationIndex(string stationIndex)
        {
            this.stationIndex = stationIndex;
        }

        public void setStationName(string stationName)
        {
            this.stationName = stationName;
        }

        public void setUpperOrDown(string upperOrDown)
        {
            this.upperOrDown = upperOrDown;
        }
    }
}

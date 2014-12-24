using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public class BusLineDirection : ISendContent
    {
        public bool readAllStation;
        public string routeNumber = "";
        public string stationName = "";
        public string upperOrDown = "";

        public static BusLineDirection deserialize(byte[] data)
        {
            BusLineDirection direction = new BusLineDirection();
            BinaryStream stream = new BinaryStream(data);
            try
            {
                direction.routeNumber = stream.readUTF();
                direction.upperOrDown = stream.readUTF();
                direction.readAllStation = stream.readBoolean();
                direction.stationName = stream.readUTF();
                stream.close();
            }
            catch (Exception)
            {
            }
            return direction;
        }

        public byte[] serialize()
        {
            BinaryStream stream = new BinaryStream();
            try
            {
                stream.writeUTF(this.routeNumber);
                stream.writeUTF(this.upperOrDown);
                stream.writeBoolean(this.readAllStation);
                stream.writeUTF(this.stationName);
                stream.close();
            }
            catch (Exception)
            {
            }
            return stream.toBytes();
        }
    }
}

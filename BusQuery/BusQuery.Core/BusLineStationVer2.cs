using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public class BusLineStationVer2
    {
        public short Id { get; set; }
        public short Order { get; set; }
        public string StationName { get; set; }
        public short UpperOrDown { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public static BusLineStationVer2 deserialize(byte[] data)
        {
            BinaryStream stream = new BinaryStream(data);
            BusLineStationVer2 ver = new BusLineStationVer2();
            try
            {
                ver.Id = stream.readShort();
                ver.Order = stream.readShort();
                ver.UpperOrDown = stream.readShort();
                ver.StationName = stream.readUTF();
                ver.X = stream.readDouble();
                ver.Y = stream.readDouble();
            }
            catch (Exception)
            {
            }
            return ver;
        }

        public byte[] serialize()
        {
            BinaryStream stream = new BinaryStream();
            try
            {
                stream.writeShort(this.Id);
                stream.writeShort(this.Order);
                stream.writeShort(this.UpperOrDown);
                stream.writeUTF(this.StationName);
                stream.writeDouble(this.X);
                stream.writeDouble(this.Y);
            }
            catch (Exception)
            {
            }
            return stream.toBytes();
        }

    }
}

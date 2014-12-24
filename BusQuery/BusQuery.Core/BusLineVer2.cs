using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public class BusLineVer2
    {
        public string CityName { get; set; }
        public string BusName { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string UpperOrDown { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public static BusLineVer2 deserialize(byte[] data)
        {
            BinaryStream stream = new BinaryStream(data);
            BusLineVer2 ver = new BusLineVer2();
            try
            {
                ver.CityName = stream.readUTF();
                ver.BusName = stream.readUTF();
                ver.BeginTime = stream.readUTF();
                ver.EndTime = stream.readUTF();
                ver.UpperOrDown = stream.readUTF();
                ver.From = stream.readUTF();
                ver.To = stream.readUTF();
                stream.close();
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
                stream.writeUTF(this.CityName);
                stream.writeUTF(this.BusName);
                stream.writeUTF(this.BeginTime);
                stream.writeUTF(this.EndTime);
                stream.writeUTF(this.UpperOrDown);
                stream.writeUTF(this.From);
                stream.writeUTF(this.To);
                stream.close();
            }
            catch (Exception)
            {
            }
            return stream.toBytes();
        }


    }
}

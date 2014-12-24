using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public class BusLineQueryResultVer2
    {
        private List<BusLineVer2> lines = new List<BusLineVer2>();

        public static BusLineQueryResultVer2 deserialize(byte[] data)
        {
            BinaryStream stream = new BinaryStream(data);
            BusLineQueryResultVer2 ver = new BusLineQueryResultVer2();
            try
            {
                int num = stream.readInt();
                List<BusLineVer2> lines = new List<BusLineVer2>();
                for (int i = 0; i < num; i++)
                {
                    int count = stream.readInt();
                    byte[] buffer = new byte[count];
                    buffer = stream.readBytes(count);
                    lines.Add(BusLineVer2.deserialize(buffer));
                }
                ver.setLines(lines);
            }
            catch (Exception)
            {
            }
            return ver;
        }

        public List<BusLineVer2> getLines()
        {
            return this.lines;
        }

        public byte[] serialize()
        {
            BinaryStream stream = new BinaryStream();
            try
            {
                stream.writeInt(this.lines.Count);
                for (int i = 0; i < this.lines.Count; i++)
                {
                    BusLineVer2 ver = this.lines[i];
                    stream.writeInt(ver.serialize().Length);
                    stream.writeBytes(ver.serialize());
                }
            }
            catch (Exception)
            {
            }
            return stream.toBytes();
        }

        public void setLines(List<BusLineVer2> lines)
        {
            this.lines = lines;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public class BusLineDirectionVector : ISendContent
    {
        private List<BusLineDirection> _busLineDirectionVector = new List<BusLineDirection>();
        private string stationName = "";

        public static BusLineDirectionVector deserialize(byte[] data)
        {
            BusLineDirectionVector vector = new BusLineDirectionVector();
            List<byte[]> list = new List<byte[]>();
            List<BusLineDirection> busLineDirectionVector = new List<BusLineDirection>();
            BusLineDirection item = new BusLineDirection();
            try
            {
                byte[] buffer;
                BinaryStream stream = new BinaryStream(data);
                int num = stream.readInt();
                for (int i = 0; i < num; i++)
                {
                    int count = stream.readInt();
                    buffer = new byte[count];
                    buffer = stream.readBytes(count);
                    list.Add(buffer);
                }
                string stationName = stream.readUTF();
                for (int j = 0; j < num; j++)
                {
                    buffer = list[j];
                    item = BusLineDirection.deserialize(buffer);
                    busLineDirectionVector.Add(item);
                }
                vector.set_BusLineDirectionVector(busLineDirectionVector);
                vector.setStationName(stationName);
                stream.close();
            }
            catch (Exception)
            {
            }
            return vector;
        }

        public List<BusLineDirection> get_BusLineDirectionVector()
        {
            return this._busLineDirectionVector;
        }

        public string getStationName()
        {
            return this.stationName;
        }

        public byte[] serialize()
        {
            byte[] buffer;
            List<byte[]> list = new List<byte[]>();
            int count = this._busLineDirectionVector.Count;
            for (int i = 0; i < count; i++)
            {
                buffer = this._busLineDirectionVector[i].serialize();
                list.Add(buffer);
            }
            BinaryStream stream = new BinaryStream();
            try
            {
                stream.writeInt(count);
                for (int j = 0; j < count; j++)
                {
                    buffer = list[j];
                    stream.writeInt(buffer.Length);
                    stream.writeBytes(buffer);
                }
                stream.writeUTF(this.stationName);
                stream.close();
            }
            catch (Exception)
            {
            }
            return stream.toBytes();
        }

        public void set_BusLineDirectionVector(List<BusLineDirection> busLineDirectionVector)
        {
            this._busLineDirectionVector = busLineDirectionVector;
        }

        public void setStationName(string stationName)
        {
            this.stationName = stationName;
        }
    }
}

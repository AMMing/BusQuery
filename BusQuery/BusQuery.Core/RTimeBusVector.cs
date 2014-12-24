using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public class RTimeBusVector : ISendContent
    {
        private List<RTimeBus> rTimeBusVector = new List<RTimeBus>();

        public static RTimeBusVector deserialize(byte[] data)
        {
            RTimeBusVector vector = new RTimeBusVector();
            List<RTimeBus> rTimeBusVector = new List<RTimeBus>();
            BinaryStream stream = new BinaryStream(data);
            try
            {
                int num = stream.readInt();
                for (int i = 0; i < num; i++)
                {
                    int count = stream.readInt();
                    byte[] buffer = new byte[count];
                    buffer = stream.readBytes(count);
                    rTimeBusVector.Add(RTimeBus.deserialize(buffer));
                }
                vector.setRTimeBusVectorVector(rTimeBusVector);
                stream.close();
            }
            catch (Exception)
            {
            }
            return vector;
        }

        public List<RTimeBus> getRouteRTimeInfoVectorVector()
        {
            return this.rTimeBusVector;
        }

        public byte[] serialize()
        {
            BinaryStream stream = new BinaryStream();
            try
            {
                stream.writeInt(this.rTimeBusVector.Count);
                for (int i = 0; i < this.rTimeBusVector.Count; i++)
                {
                    RTimeBus bus = this.rTimeBusVector[i];
                    stream.writeInt(bus.serialize().Length);
                    stream.writeBytes(bus.serialize());
                }
                stream.close();
            }
            catch (Exception)
            {
            }
            return stream.toBytes();
        }

        public void setRTimeBusVectorVector(List<RTimeBus> rTimeBusVector)
        {
            this.rTimeBusVector = rTimeBusVector;
        }
    }
}

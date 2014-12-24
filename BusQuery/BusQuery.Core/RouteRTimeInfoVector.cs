using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public class RouteRTimeInfoVector : ISendContent
    {
        private List<RouteRTimeInfo> rTimeVector = new List<RouteRTimeInfo>();

        public static RouteRTimeInfoVector deserialize(byte[] data)
        {
            RouteRTimeInfoVector vector = new RouteRTimeInfoVector();
            List<RouteRTimeInfo> rTimeVector = new List<RouteRTimeInfo>();
            BinaryStream stream = new BinaryStream(data);
            try
            {
                int num = stream.readInt();
                for (int i = 0; i < num; i++)
                {
                    int count = stream.readInt();
                    byte[] buffer = new byte[count];
                    buffer = stream.readBytes(count);
                    rTimeVector.Add(RouteRTimeInfo.deserialize(buffer));
                }
                vector.setRTimeVectorVector(rTimeVector);
                stream.close();
            }
            catch (Exception)
            {
            }
            return vector;
        }

        public List<RouteRTimeInfo> getRouteRTimeInfoVectorVector()
        {
            return this.rTimeVector;
        }

        public byte[] serialize()
        {
            BinaryStream stream = new BinaryStream();
            try
            {
                stream.writeInt(this.rTimeVector.Count);
                for (int i = 0; i < this.rTimeVector.Count; i++)
                {
                    RouteRTimeInfo info = this.rTimeVector[i];
                    stream.writeInt(info.serialize().Length);
                    stream.writeBytes(info.serialize());
                }
                stream.close();
            }
            catch (Exception)
            {
            }
            return stream.toBytes();
        }

        public void setRTimeVectorVector(List<RouteRTimeInfo> rTimeVector)
        {
            this.rTimeVector = rTimeVector;
        }
    }
}

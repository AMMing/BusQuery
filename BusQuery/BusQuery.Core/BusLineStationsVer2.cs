using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public class BusLineStationsVer2
    {
        private List<BusLineStationVer2> stations;

        public static BusLineStationsVer2 deserialize(byte[] data)
        {
            BinaryStream stream = new BinaryStream(data);
            BusLineStationsVer2 ver = new BusLineStationsVer2();
            try
            {
                int num = stream.readInt();
                List<BusLineStationVer2> stations = new List<BusLineStationVer2>();
                for (int i = 0; i < num; i++)
                {
                    int count = stream.readInt();
                    byte[] buffer = new byte[count];
                    buffer = stream.readBytes(count);
                    stations.Add(BusLineStationVer2.deserialize(buffer));
                }
                ver.setStations(stations);
            }
            catch (Exception)
            {
            }
            return ver;
        }

        public List<BusLineStationVer2> getStations()
        {
            return this.stations;
        }

        public byte[] serialize()
        {
            BinaryStream stream = new BinaryStream();
            try
            {
                stream.writeInt(this.stations.Count);
                for (int i = 0; i < this.stations.Count; i++)
                {
                    BusLineStationVer2 ver = this.stations[i];
                    stream.writeInt(ver.serialize().Length);
                    stream.writeBytes(ver.serialize());
                }
            }
            catch (Exception)
            {
            }
            return stream.toBytes();
        }

        public void setStations(List<BusLineStationVer2> stations)
        {
            this.stations = stations;
        }
    }
}

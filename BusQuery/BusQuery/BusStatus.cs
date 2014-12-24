using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BusQuery
{
    public class BusStatus
    {

        public BusStatus(Core.RTimeBus info)
        {
            switch (int.Parse(info.getStatusType()))
            {
                case 0:
                    Uri = "/images/1.png";
                    break;
                case 2:
                    Uri = "/images/2.png";
                    break;
                default:
                    Uri = string.Empty;
                    break;
            }
        }


        public string Uri { get; set; }
    }
}

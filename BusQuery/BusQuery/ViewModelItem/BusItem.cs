using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BusQuery.ViewModelItem
{
    public class BusItem : Core.BusLineVer2
    {
        public BusItem(Core.BusLineVer2 ver)
        {
            base.CityName = ver.CityName;
            base.BusName = ver.BusName;
            base.BeginTime = ver.BeginTime;
            base.EndTime = ver.EndTime;
            base.UpperOrDown = ver.UpperOrDown;
            base.From = ver.From;
            base.To = ver.To;
            CollectCommend = new CommandEx<BusItem>(item =>
            {
                MessageBox.Show(item.BusName);
            });
        }


        public CommandEx<BusItem> CollectCommend { get; set; }
    }
}

using MetroWPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BusQuery
{
    public class BusTabItem : TabItem
    {
        public BusTabItem(Core.BusLineVer2 ver2)
        {
            this.TabItemControl = new TabItemControl(ver2);
            MetroControl = new MetroContentControl();
            MetroControl.Content = this.TabItemControl;

            this.Header = new TextBlock
            {
                Text = ver2.BusName,
                Style = Application.Current.Resources["MetroTopTitle"] as Style
            };
            this.Content = MetroControl;
        }

        private MetroContentControl MetroControl { get; set; }

        private TabItemControl TabItemControl { get; set; }


    }
}

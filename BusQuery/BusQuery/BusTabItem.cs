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
            this.BusID = ver2.BusName;
            this.BusFromTo = string.Format("[{0}]→[{1}]", ver2.From.Substring(0, 1), ver2.To.Substring(0, 1));
            var header = new TabItemHeader
            {
                Header = this.BusID,
                HeaderMini = this.BusFromTo
            };
            header.Close += header_Close;
            this.Header = header;
            this.Content = MetroControl;

        }

        public event EventHandler<RoutedEventArgs> Close;

        void header_Close(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Close");
            if (Close != null)
            {
                Close(this, e);
            }
        }

        private MetroContentControl MetroControl { get; set; }

        private TabItemControl TabItemControl { get; set; }

        public string Key
        {
            get
            {
                return string.Format("{0}{1}", this.BusID, this.BusFromTo);
            }
        }

        public string BusID { get; set; }

        public string BusFromTo { get; set; }
    }
}

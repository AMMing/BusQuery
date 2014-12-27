using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BusQuery
{
    /// <summary>
    /// BusStation_Control.xaml 的交互逻辑
    /// </summary>
    public partial class BusStation_Control : UserControl
    {
        public BusStation_Control()
        {
            InitializeComponent();
        }

        public BusStation_Control(Core.BusLineStationVer2 ver2)
            : this()
        {
            BusLineStation = ver2;

            Load();
        }
        public string Key { get; private set; }

        private Core.BusLineStationVer2 _busLineStation;

        public Core.BusLineStationVer2 BusLineStation
        {
            get { return _busLineStation; }
            set
            {
                _busLineStation = value;
            }
        }


        private void Load()
        {
            txt_name.Text = BusLineStation.StationName;
        }

        public void ClearBus()
        {
            lb_bus.Items.Clear();
        }

        public void CheckBus(Core.RTimeBus info)
        {
            if (BusLineStation.StationName == info.getStationName())
            {
                lb_bus.Items.Add(new BusStatus(info));
                if (chb_toast.IsChecked.Value)
                {
                    MessageHelper.Current.AppendToastMessage("车到了", "车到了车到了车到了车到了车到了车到了车到了车到了车到了");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BusQuery
{
    /// <summary>
    /// TabItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class TabItemControl : UserControl
    {
        public TabItemControl()
        {
            InitializeComponent();
        }
        public TabItemControl(Core.BusLineVer2 ver2)
            : this()
        {
            BusLineVer2 = ver2;

            //this.Title = string.Format("[{0}] {1} => {2}", BusLineVer2.BusName, BusLineVer2.From, BusLineVer2.To);

            this.Loaded += Window_BusStatus_Loaded;
        }


        private DispatcherTimer timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(5)
        };
        Core.BusLineVer2 BusLineVer2 = new Core.BusLineVer2();
        Dictionary<string, BusStation_Control> busStationDic = new Dictionary<string, BusStation_Control>();
        Core.Helper helper = new Core.Helper();


        void Window_BusStatus_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
            timer.Tick += timer_Tick;
            timer.Start();
            BusNow();
        }


        private void BusNow()
        {
            helper.GetBusNow(BusLineVer2, (isSrccess, list) =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (isSrccess && list.Count > 0)
                        {
                            var busnow = list[0];
                            if (string.IsNullOrWhiteSpace(txt.Text))
                            {
                                txt.Text = string.Format("{0}   {4} => {5}\n{1} 到 {2} \n{3}", busnow.getRouteName(),
                                   busnow.getBeginTime(), busnow.getEndTime(), busnow.getComments(),
                                   BusLineVer2.From, BusLineVer2.To);
                            }

                            var rtime = busnow.getrTimeBusVector();

                            foreach (var item in busStationDic)
                            {
                                item.Value.ClearBus();
                            }

                            foreach (var item in rtime.getRouteRTimeInfoVectorVector())
                            {
                                var sname = item.getStationName();
                                if (busStationDic.ContainsKey(sname))
                                {
                                    var s = busStationDic[sname];
                                    s.CheckBus(item);
                                }
                            }
                        }

                    }
                    catch (Exception)
                    {
                    }
                }));
            });
        }

        void timer_Tick(object sender, EventArgs e)
        {
            BusNow();
        }

        private void Load()
        {
            helper.GetStationList(BusLineVer2, (isSrccess, list) =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (isSrccess)
                    {
                        busStationDic.Clear();
                        foreach (var item in list)
                        {
                            busStationDic.Add(item.StationName, new BusStation_Control(item));
                        }
                        listbox.Items.Clear();
                        foreach (var item in busStationDic)
                        {
                            listbox.Items.Add(item.Value);
                        }
                    }
                    else
                    {
                        MessageBox.Show("加载站点失败 请重试");
                    }
                }));
            });
        }
    }
}

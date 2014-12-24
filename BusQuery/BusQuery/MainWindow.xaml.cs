using MetroWPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace BusQuery
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.btn_search.Click += btn_search_Click;
            this.listbox.MouseDoubleClick += listbox_MouseDoubleClick;
            this.txt_kw.KeyUp += txt_kw_KeyUp;
            this.txt_kw.Focus();

            toastHelper.IsAppLinkExists();
            MessageHelper.Current.BarChange += Current_BarChange;
        }

        void Current_BarChange(string obj)
        {
            txt_Status.Text = obj;
        }

        void txt_kw_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btn_search_Click(null, null);
            }
        }

        void listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = this.listbox.SelectedItem as Core.BusLineVer2;
            this.listbox.SelectedItem = null;
            if (item != null)
            {
                tabControl.Items.Add(new BusTabItem(item));
                tabControl.SelectedIndex = tabControl.Items.Count - 1;
            }
        }

        Core.Helper heler = new Core.Helper();
        ToastHelper toastHelper = new ToastHelper();
        private void Search()
        {
            heler.GetLuXian(txt_kw.Text, (isSuccess, list) =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (isSuccess)
                    {
                        this.listbox.Items.Clear();
                        foreach (var item in list)
                        {
                            this.listbox.Items.Add(new ViewModelItem.BusItem(item));
                        }
                    }
                    else
                    {
                        MessageHelper.Current.AppendBarMessage("搜索失败 请重试", true);
                    }
                    tabControl.SelectedIndex = 0;
                }));
            });
        }

        void btn_search_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }


    }
}

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

namespace BusQuery
{
    /// <summary>
    /// TabItemHeader.xaml 的交互逻辑
    /// </summary>
    public partial class TabItemHeader : UserControl
    {
        public TabItemHeader()
        {
            InitializeComponent();
        }
        public string Header
        {
            get
            {
                return txt.Text;
            }
            set
            {
                txt.Text = value;
            }
        }

        public string HeaderMini
        {
            get
            {
                return txt2.Text;
            }
            set
            {
                txt2.Text = value;
            }
        }
        public event EventHandler<RoutedEventArgs> Close;
        private void OnClose()
        {
            if (Close != null)
            {
                Close(this, null);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnClose();
        }

        private void userControlTabItemHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                OnClose();
            }

        }
    }
}

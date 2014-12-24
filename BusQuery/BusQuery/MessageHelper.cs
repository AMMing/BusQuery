using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BusQuery
{
    public class MessageHelper
    {
        static MessageHelper()
        {
            Current = new MessageHelper();
        }

        public static MessageHelper Current { get; set; }


        public event Action<string> BarChange;

        private void OnBarChange(string msg)
        {
            if (BarChange != null)
            {
                BarChange(msg);
            }
        }

        public void AppendToastMessage(string title, string msg)
        {
            ToastHelper ToastHelper = new ToastHelper();
            ToastHelper.Toast(title, msg);
        }
        public void AppendBarMessage(string msg, bool showBox = false)
        {
            OnBarChange(msg);
            if (showBox)
            {
                MessageBox.Show(msg);
            }
        }
    }
}

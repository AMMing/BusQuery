
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using MS.WindowsAPICodePack.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace BusQuery
{
    public class ToastHelper
    {
        private const String APP_ID = "BusQuery - DesktopAppNotification";
        public bool IsAppLinkExists()
        {
            string defaultPath = string.Format(@"{0}\Microsoft\Windows\Start Menu\Programs\{1}.lnk",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                APP_ID);

            return File.Exists(defaultPath) == false ? CreateApplicationShortcut(defaultPath) : true;
        }

        private bool CreateApplicationShortcut(string defaultPath)
        {
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            var newShortcut = (IShellLinkW)new CShellLink();

            // Create a shortcut to the exe
            ErrorHelper.VerifySucceeded(newShortcut.SetPath(exePath));
            ErrorHelper.VerifySucceeded(newShortcut.SetArguments(""));

            // Open the shortcut property store, set the AppUserModelId property
            var newShortcutProperties = (IPropertyStore)newShortcut;

            using (PropVariant appId = new PropVariant(APP_ID))
            {
                ErrorHelper.VerifySucceeded(newShortcutProperties.SetValue(SystemProperties.System.AppUserModel.ID, appId));
                ErrorHelper.VerifySucceeded(newShortcutProperties.Commit());
            }

            // Commit the shortcut to disk
            var newShortcutSave = (IPersistFile)newShortcut;

            ErrorHelper.VerifySucceeded(newShortcutSave.Save(defaultPath, true));
            return true;
        }


        public void Toast(string title, string des)
        {
            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);
            var textFields = toastXml.GetElementsByTagName("text");
            textFields[0].AppendChild(toastXml.CreateTextNode(title));
            textFields[1].AppendChild(toastXml.CreateTextNode(des));
            String imagePath = "file:///" + System.IO.Path.GetFullPath("tosat.png");
            XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier(APP_ID).Show(toast);
        }
    }
}

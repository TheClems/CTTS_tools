using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System.Profile;
using Windows.System.Diagnostics;
using System.Management;
using log4net.Util;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace CTTS_Tools
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class DevicePage : Page
    {
        public DevicePage()
        {
            this.InitializeComponent();
            GetDeviceInformation();
        }

        private void GetDeviceInformation()
        {
            var deviceInfo = new EasClientDeviceInformation();

            DeviceId.Text = deviceInfo.Id.ToString();
            OperatingSystem.Text = deviceInfo.OperatingSystem;
            ComputerName.Text = deviceInfo.FriendlyName;
            Manufacturer.Text = deviceInfo.SystemManufacturer;


            // Informations réseau
            IpAdress.Text = this.GetIpAddress();
            InternetStatus.Text = GetInternetStatus();

        }

        private string GetIpAddress()
        {
            try
            {
                var hostnames = Windows.Networking.Connectivity.NetworkInformation.GetHostNames();
                foreach (var hn in hostnames)
                {
                    if (hn.IPInformation != null)
                    {
                        return hn.DisplayName;
                    }
                }
                return "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        private string GetInternetStatus()
        {
            try
            {
                return NetworkInterface.GetIsNetworkAvailable() ? "Connecté" : "Déconnecté";
            }
            catch
            {
                return "N/A";
            }
        }
    }
}

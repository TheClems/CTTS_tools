using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace CTTS_Tools
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            ShowDisplayNameAsync();
        }

        private async void ShowDisplayNameAsync()
        {
            var users = await User.FindAllAsync();
            var currentUser = users.Where(user =>
            user.AuthenticationStatus == UserAuthenticationStatus.LocallyAuthenticated && user.Type == UserType.LocalUser).FirstOrDefault();

            if (currentUser != null)
            {
                var displayName = await currentUser.GetPropertyAsync(KnownUserProperties.DisplayName);
                WelcomeText.Text = $"Bonjour {displayName}";
            }
        }
    }
}

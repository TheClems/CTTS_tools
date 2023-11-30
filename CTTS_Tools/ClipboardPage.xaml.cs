using CTTS_Tools.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ClipboardPage : Page
    {
        private ObservableCollection<HistoryItem> historyItems;

        public ClipboardPage()
        {
            InitializeComponent();
            historyItems = new ObservableCollection<HistoryItem>();
            lvHistoryItems.ItemsSource = historyItems;

            if (Clipboard.IsHistoryEnabled())
            {
                Clipboard.HistoryChanged += ClipboardHistoryChanged;
            }

            GetHistoryItemsAsync();
        }

        private async void ClipboardHistoryChanged(object sender, ClipboardHistoryChangedEventArgs e)
        {
            var content = Clipboard.GetContent();

            if (content.Contains(StandardDataFormats.Text))
            {
                var text = await content.GetTextAsync();
                historyItems.Insert(0, new HistoryItem { Text = text, TimeStamp = new DateTimeOffset() });
            }
        }

        private async void GetHistoryItemsAsync()
        {
            var history = await Clipboard.GetHistoryItemsAsync();

            foreach (var item in history.Items)
            {
                var text = await item.Content.GetTextAsync();
                historyItems.Add(new HistoryItem { Text = text, TimeStamp = item.Timestamp });
            }
        }
    }
}

using ShopApp.Utils;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp.Pages;

public partial class AboutPage : Page {
    private readonly MainWindow mainWindow;

    public AboutPage() {
        InitializeComponent();
        mainWindow = UiHelper.GetMainWindow();
    }

    private void BackButton_Click(object sender, RoutedEventArgs e) => mainWindow.Navigate<ItemBrowserPage>();
}

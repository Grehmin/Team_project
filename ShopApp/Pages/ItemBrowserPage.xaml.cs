using ShopApp.Utils;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp.Pages;

public partial class ItemBrowserPage : Page {
    private readonly MainWindow mainWindow;

    public ItemBrowserPage() {
        InitializeComponent();
        mainWindow = UiHelper.GetMainWindow();
    }

    private void CartButton_Click(object sender, RoutedEventArgs e) => mainWindow.Navigate<CartPage>();
    private void ProfileButton_Click(object sender, RoutedEventArgs e) => mainWindow.Navigate<ProfilePage>();
    private void AboutButton_Click(object sender, RoutedEventArgs e) => mainWindow.Navigate<AboutPage>();
}

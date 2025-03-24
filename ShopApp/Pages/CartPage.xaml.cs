using ShopApp.Utils;
using System.Windows.Controls;

namespace ShopApp.Pages;

public partial class CartPage : Page {
    private readonly MainWindow mainWindow;

    public CartPage() {
        InitializeComponent();
        mainWindow = UiHelper.GetMainWindow();
    }

    private void ItemBrowserButton_Click(object sender, System.Windows.RoutedEventArgs e) => mainWindow.Navigate<ItemBrowserPage>();
    private void ProfileButton_Click(object sender, System.Windows.RoutedEventArgs e) => mainWindow.Navigate<ProfilePage>();
}

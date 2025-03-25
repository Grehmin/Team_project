using ShopApp.Utils;
using System.Windows.Controls;

namespace ShopApp.Pages;

public partial class ProfilePage : Page {
    private readonly MainWindow mainWindow;

    public ProfilePage() {
        InitializeComponent();
        mainWindow = UiHelper.GetMainWindow();
    }

    private void ItemBrowserButton_Click(object sender, System.Windows.RoutedEventArgs e) => mainWindow.Navigate<ItemBrowserPage>();
    private void OrderHistoryButton_Click(object sender, System.Windows.RoutedEventArgs e) => mainWindow.Navigate<OrderHistoryPage>();

    private void EditProfileButton_Click(object sender, System.Windows.RoutedEventArgs e) {

    }

    private void SavePreferencesButton_Click(object sender, System.Windows.RoutedEventArgs e) {

    }
}

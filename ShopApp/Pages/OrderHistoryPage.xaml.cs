using ShopApp.Utils;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp.Pages;

public partial class OrderHistoryPage : Page {
    private readonly MainWindow mainWindow;

    public OrderHistoryPage() {
        InitializeComponent();
        mainWindow = UiHelper.GetMainWindow();
    }

    private void ProfileButton_Click(object sender, RoutedEventArgs e) => mainWindow.Navigate<ProfilePage>();
}

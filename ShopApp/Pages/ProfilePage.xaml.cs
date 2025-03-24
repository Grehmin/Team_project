using ShopApp.Database;
using ShopApp.Utils;
using System.Windows.Controls;

namespace ShopApp.Pages;

public partial class ProfilePage : Page {
    private readonly MainWindow mainWindow;
    public AppDbContext? Context;
    public User? CurrentUser { get; set; }

    public ProfilePage() {
        InitializeComponent();
        mainWindow = UiHelper.GetMainWindow();
        this.DataContext = this;
    }

    private User GetUser() {
        if (CurrentUser == null) throw new Exception("User is null");
        return CurrentUser;
    }
    private AppDbContext GetContext() {
        if (Context == null) throw new Exception("Context is null");
        return Context;
    }

    private void ItemBrowserButton_Click(object sender, System.Windows.RoutedEventArgs e) => mainWindow.Navigate<ItemBrowserPage>();
    private void OrderHistoryButton_Click(object sender, System.Windows.RoutedEventArgs e) => mainWindow.Navigate<OrderHistoryPage>();

    private void EditProfileButton_Click(object sender, System.Windows.RoutedEventArgs e) {

    }

    private void SavePreferencesButton_Click(object sender, System.Windows.RoutedEventArgs e) {

    }
}

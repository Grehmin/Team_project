using ShopApp.Database;
using ShopApp.Pages;
using System.Windows;

namespace ShopApp;

public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e) {
        MainFrame.Navigate(new AuthenticationPage());
        //await LoadUsers();
    }

    private async Task LoadUsers() {
        var context = await AppDbContext.InitializeAsync();
        var users = context.Users.ToList();
        UsersDataGrid.ItemsSource = users;
    }

}
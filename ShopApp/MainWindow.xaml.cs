using ShopApp.Database;
using ShopApp.Pages;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp;

public partial class MainWindow : Window {
    private readonly Dictionary<Type, Page> pages = [];

    public MainWindow() {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e) {
        Navigate<AuthenticationPage>();
    }

    public void Navigate<T>() where T : Page, new() {
        Type pageType = typeof(T);
        if (!pages.TryGetValue(pageType, out var page)) {
            page = new T();
            pages.Add(pageType, page);
        }
        MainFrame.Navigate(page);
    }

    private async Task LoadUsers() {
        var context = await AppDbContext.InitializeAsync();
        var users = context.Users.ToList();
        UsersDataGrid.ItemsSource = users;
    }

}
using ShopApp.Database;
using ShopApp.Pages;
using System.Diagnostics;
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

    private Page RegisterPage<T>() where T : Page, new() {
        Type pageType = typeof(T);
        if (!pages.TryGetValue(pageType, out var page)) {
            page = new T();
            pages.Add(pageType, page);
        }
        return page;
    }

    public void Navigate<T>() where T : Page, new() {
        var page = RegisterPage<T>();
        MainFrame.Navigate(page);
    }
    public void Navigate<T>(User user) where T : Page, new() {
        var page = RegisterPage<T>();
        if (page is ItemBrowserPage browserPage) {
            browserPage.CurrentUser = user;
            MainFrame.Navigate(browserPage);
            return;
        }
        throw new UnreachableException();
    }
    public void Navigate<T>(User User, AppDbContext Context) where T : Page, new() {
        var page = RegisterPage<T>();
        if (page is CartPage cartPage) {
            cartPage.CurrentUser = User;
            cartPage.Context = Context;
            MainFrame.Navigate(cartPage);
            return;
        }
        throw new UnreachableException();
    }

    private async Task LoadUsers() {
        var context = await AppDbContext.InitializeAsync();
        var users = context.Users.ToList();
        UsersDataGrid.ItemsSource = users;
    }

}
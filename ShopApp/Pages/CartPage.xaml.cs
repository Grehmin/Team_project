using Microsoft.EntityFrameworkCore;
using ShopApp.Database;
using ShopApp.Utils;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ShopApp.Pages;

public partial class CartPage : Page {
    private readonly MainWindow mainWindow;
    public User? CurrentUser { get; set; }
    public AppDbContext? Context { get; set; }
    public ObservableCollection<Cart> CartItems { get; set; } = [];

    public CartPage() {
        InitializeComponent();
        mainWindow = UiHelper.GetMainWindow();
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e) {
        await RefreshCart();
    }

    private User GetUser() {
        if (CurrentUser == null) throw new Exception("User is null");
        return CurrentUser;
    }
    private AppDbContext GetContext() {
        if (Context == null) throw new Exception("Context is null");
        return Context;
    }

    private async Task RefreshCart() {
        CartItems = [.. await GetContext().Carts
            .Where(c => c.UserId == GetUser().UserID)
            .Include(c => c.Product)
            .ToListAsync()];
        CartItemsView.ItemsSource = CartItems;
    }



    private void ItemBrowserButton_Click(object sender, System.Windows.RoutedEventArgs e) => mainWindow.Navigate<ItemBrowserPage>();
    private void ProfileButton_Click(object sender, System.Windows.RoutedEventArgs e) => mainWindow.Navigate<ProfilePage>();

}

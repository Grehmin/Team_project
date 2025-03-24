using Microsoft.EntityFrameworkCore;
using ShopApp.Database;
using ShopApp.Utils;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopApp.Pages;

public partial class ItemBrowserPage : Page {
    private readonly MainWindow mainWindow;
    private AppDbContext? Context;
    public User? CurrentUser { get; set; }
    public ObservableCollection<Product> Products { get; set; } = [];

    public ItemBrowserPage() {
        InitializeComponent();
        mainWindow = UiHelper.GetMainWindow();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e) {
        Context = await AppDbContext.InitializeAsync();
        // fetch data 
        Products = [.. await Task.Run(Context.Products.ToList)];
        // set sources
        ProductsItemsControl.ItemsSource = Products;
        // adjust ui
        if (GetUser().IsAdmin) {
            AddNewProductButton.Visibility = Visibility.Visible;
        }
    }

    private User GetUser() {
        if (CurrentUser == null) throw new Exception("User is null");
        return CurrentUser;
    }
    private AppDbContext GetContext() {
        if (Context == null) throw new Exception("Context is null");
        return Context;
    }

    private void CartButton_Click(object sender, RoutedEventArgs e) => mainWindow.Navigate<CartPage>(GetUser(), GetContext());
    private void ProfileButton_Click(object sender, RoutedEventArgs e) => mainWindow.Navigate<ProfilePage>(GetUser(), GetContext());
    private void AboutButton_Click(object sender, RoutedEventArgs e) => mainWindow.Navigate<AboutPage>();

    private async void AddToCart_Click(object sender, RoutedEventArgs e) {
        if (Context == null || CurrentUser == null) throw new Exception("Context or CurrentUser is null");
        if (sender is Button button && button.Tag is int productId) {
            try {
                var existingCartItem = await Context.Carts.FirstOrDefaultAsync(c => c.UserId == CurrentUser.UserID && c.ProductId == productId);
                string msg = "";
                if (existingCartItem != null) {
                    existingCartItem.Quantity++;
                    msg = $"TEMPORARY: Item Quantity in cart updated: {existingCartItem.Quantity}";
                } else {
                    msg = "TEMPORARY: Product added to cart!";
                    var newCartItem = new Cart {
                        UserId = CurrentUser.UserID,
                        ProductId = productId,
                        Quantity = 1,
                        AddedAt = DateTime.UtcNow
                    };
                    await Context.Carts.AddAsync(newCartItem);
                }
                await Context.SaveChangesAsync();
                MessageBox.Show(msg, "Success", MessageBoxButton.OK, MessageBoxImage.Information); // TODO(d4nse): Remove later
            } catch (Exception) {
                throw;
            }
        }
    }

    private void AddNewProductButton_Click(object sender, RoutedEventArgs e) {
        if (!GetUser().IsAdmin) throw new Exception("Current user is not admin, and this button should be invisible");
        var page = new ProductEditorPage(GetContext(), null);
        mainWindow.MainFrame.Navigate(page);
    }

    // Edit product on right mouse click
    private void Product_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
        if (!GetUser().IsAdmin) return;
        if (sender is Border clickedItem) {
            if (clickedItem.DataContext is Product product) {
                var page = new ProductEditorPage(GetContext(), product);
                mainWindow.MainFrame.Navigate(page);
            }
        }

    }
}

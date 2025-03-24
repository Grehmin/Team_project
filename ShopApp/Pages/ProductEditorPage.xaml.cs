using Microsoft.Win32;
using ShopApp.Database;
using ShopApp.Utils;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp.Pages;

public partial class ProductEditorPage : Page {
    private readonly AppDbContext Context;
    private readonly Product? ExistingProduct;

    public ProductEditorPage(AppDbContext Context, Product? Product = null) {
        InitializeComponent();
        this.Context = Context;
        ExistingProduct = Product;
    }

    private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e) {
        if (ExistingProduct != null) {
            txtName.Text = ExistingProduct.ProductName;
            txtDescription.Text = ExistingProduct.ProductDescription;
            txtPrice.Text = ExistingProduct.Price.ToString("0.00");
            txtQuantity.Text = ExistingProduct.Quantity.ToString();
            txtImage.Text = ExistingProduct.Image;
        }
    }

    private void BrowseImage_Click(object sender, RoutedEventArgs e) {
        var dialog = new OpenFileDialog {
            Filter = "Image files (*.png;*.jpg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
        };
        if (dialog.ShowDialog() == true) {
            txtImage.Text = dialog.FileName;
        }
    }

    private string? SaveImageToAppImages() {
        if (string.IsNullOrWhiteSpace(txtImage.Text)) return null;
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string stem = Path.GetFileName(txtName.Text);
        string newFilename = $"{timestamp}{stem}";
        var dest = Paths.ToImage(newFilename);
        File.Copy(txtImage.Text, dest);
        return newFilename;
    }

    private async void Save_Click(object sender, RoutedEventArgs e) {
        if (!ValidateInput()) return;

        try {
            if (ExistingProduct == null) {
                // Create new product
                var newFilename = SaveImageToAppImages();
                var newProduct = new Product {
                    ProductName = txtName.Text,
                    ProductDescription = txtDescription.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    Quantity = int.Parse(txtQuantity.Text),
                    Image = newFilename,
                    CreatedAtProduct = DateTime.UtcNow
                };
                await Context.Products.AddAsync(newProduct);
            } else {
                // Update existing product
                var newFilename = SaveImageToAppImages();
                ExistingProduct.ProductName = txtName.Text;
                ExistingProduct.ProductDescription = txtDescription.Text;
                ExistingProduct.Price = decimal.Parse(txtPrice.Text);
                ExistingProduct.Quantity = int.Parse(txtQuantity.Text);
                ExistingProduct.Image = newFilename;
                Context.Products.Update(ExistingProduct);
            }

            await Context.SaveChangesAsync();
            NavigationService.GoBack();
        } catch (Exception ex) {
            MessageBox.Show($"Error saving product: {ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool ValidateInput() {
        if (string.IsNullOrWhiteSpace(txtName.Text)) {
            MessageBox.Show("Product name is required!", "Validation Error",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0) {
            MessageBox.Show("Invalid price! Must be a positive number.",
                "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0) {
            MessageBox.Show("Invalid quantity! Must be zero or positive.",
                "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        return true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e) {
        NavigationService.GoBack();
    }
}

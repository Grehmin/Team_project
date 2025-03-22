using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using System.Windows;

namespace ShopApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        LoadUsers();
    }
    private void LoadUsers()
    {
        using (var context = new AppDbContext())
        {
            var users = context.Users.ToList();
            UsersDataGrid.ItemsSource = users;
            UsersDataGrid.ItemsSource = users;          
        }
    }
}
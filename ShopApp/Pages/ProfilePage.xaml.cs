using ShopApp.Database;
using ShopApp.Utils;
using System.Windows;
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

    private void ChangeLang_RU_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        ResourceDictionary dict = new ResourceDictionary();
        dict.Source = new Uri("..\\Assets\\Dictionary-ru-RU.xaml", UriKind.Relative);

        RefreshLang(dict);
    }

    private void ChangeLang_EN_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        ResourceDictionary dict = new ResourceDictionary();
        dict.Source = new Uri("..\\Assets\\Dictionary-en-US.xaml", UriKind.Relative);

        RefreshLang(dict);
    }

    private void RefreshLang(ResourceDictionary _dict)
    {
        var oldDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.StartsWith("/Assets/Dictionary"));

        if (oldDict != null)
        {
            Application.Current.Resources.MergedDictionaries.Remove(oldDict);
        }

        Application.Current.Resources.MergedDictionaries.Add(_dict);

    }
}

using Microsoft.EntityFrameworkCore;
using ShopApp.Database;
using ShopApp.Utils;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp.Pages;

public partial class AuthenticationPage : Page {
    private readonly MainWindow mainWindow;
    private AppDbContext? context;
    private FormInputMode currentMode;

    public AuthenticationPage() {
        InitializeComponent();
        mainWindow = UiHelper.GetMainWindow();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e) {
        context = await AppDbContext.InitializeAsync();
    }

    // returns true if switched, false if didnt switch
    private bool SwitchInputMode(FormInputMode mode) {
        if (currentMode == mode) return false;
        if (mode == FormInputMode.SignIn) {
            SetInputFormVisibility(EmailLabel, EmailTextBox, Visibility.Collapsed);
            SetInputFormVisibility(PhoneLabel, PhoneTextBox, Visibility.Collapsed);
        } else if (mode == FormInputMode.SignUp) {
            SetInputFormVisibility(EmailLabel, EmailTextBox, Visibility.Visible);
            SetInputFormVisibility(PhoneLabel, PhoneTextBox, Visibility.Visible);
        }
        currentMode = mode;
        return true;
    }

    private static void SetInputFormVisibility(Label label, TextBox textbox, Visibility visibility) {
        label.Visibility = visibility;
        textbox.Visibility = visibility;
    }

    private async void SignInButton_Click(object sender, RoutedEventArgs e) {
        if (SwitchInputMode(FormInputMode.SignIn)) return; // change ui
        HintTextBlock.Text = Paths.GetString("Page_Authentication_Hint_Login_OngoingAction");
        string login = LoginTextBox.Text;
        string password = PasswordTextBox.Text;
        var ret = await LoginAsync(login, password);
        if (ret.Success) {
            HintTextBlock.Text = Paths.GetString("Page_Authentication_Hint_Login_Success");
            if (ret.User == null) throw new Exception("User is null after successfull auth");
            mainWindow.Navigate<ItemBrowserPage>(ret.User);
        } else {
            HintTextBlock.Text = ret.Error;
        }
    }

    private async void SignUpButton_Click(object sender, RoutedEventArgs e) {
        if (SwitchInputMode(FormInputMode.SignUp)) return; // change ui
        HintTextBlock.Text = Paths.GetString("Page_Authentication_Hint_Register_OngoingAction");
        string login = LoginTextBox.Text;
        string password = PasswordTextBox.Text;
        string email = EmailTextBox.Text;
        string phone = PhoneTextBox.Text;
        var ret = await RegisterAsync(login, password, email, phone);
        if (ret.Success) {
            HintTextBlock.Text = Paths.GetString("Page_Authentication_Hint_Register_Success");
            if (ret.User == null) throw new Exception("User is null after successfull auth");
            mainWindow.Navigate<ItemBrowserPage>(ret.User);
        } else {
            HintTextBlock.Text = ret.Error;
        }
    }

    public async Task<(bool Success, User? User, string? Error)> RegisterAsync(
        string login,
        string password,
        string email,
        string phoneNumber,
        string? fullName = null,
        string? address = null) {
        try {
            if (context == null) { return (false, null, Paths.GetString("Page_Authentication_Hint_DatabaseIsLate")); }
            if (await context.Users.AnyAsync(u => u.Login == login || u.Email == email)) {
                return (false, null, Paths.GetString("Page_Authentication_Hint_UserExists"));
            }
            var newUser = new User {
                Login = login,
                Password = password,
                Email = email,
                FullName = fullName,
                Address = address,
                PhoneNumber = phoneNumber
            };
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
            return (true, newUser, null);
        } catch (Exception) {
            throw;
            //return (false, null, $"Registration failed: {ex.Message}");
        }
    }

    public async Task<(bool Success, User? User, string? Error)> LoginAsync(string loginOrEmail, string password) {
        try {
            if (context == null) { return (false, null, Paths.GetString("Page_Authentication_Hint_DatabaseIsLate")); }
            var user = await context.Users.FirstOrDefaultAsync(u => u.Login == loginOrEmail || u.Email == loginOrEmail);
            if (user == null) {
                return (false, null, Paths.GetString("Page_Authentication_Hint_UserNotExists"));
            }
            if (!user.Password.Equals(password)) {
                return (false, null, Paths.GetString("Page_Authentication_Hint_UserNotExists"));
            }
            return (true, user, null);
        } catch (Exception) {
            throw;
            //return (false, null, $"Login failed: {ex.Message}");
        }
    }

    private enum FormInputMode {
        SignIn,
        SignUp,
    }

    private void ChangeLang_RU_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        ResourceDictionary dict = new ResourceDictionary();
        dict.Source = new Uri("..\\Assets\\Dictionary-ru-RU.xaml", UriKind.Relative);

        RefreshLang(dict);
    }

    private void ChangeLang_EN_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        ResourceDictionary dict = new ResourceDictionary();
        dict.Source = new Uri("..\\Assets\\Dictionary-en-US.xaml", UriKind.Relative);

        RefreshLang(dict);
    }

    private void RefreshLang(ResourceDictionary _dict) {
        var oldDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.StartsWith("/Assets/Dictionary"));

        if (oldDict != null) {
            Application.Current.Resources.MergedDictionaries.Remove(oldDict);
        }

        Application.Current.Resources.MergedDictionaries.Add(_dict);

    }
}


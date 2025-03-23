using ShopApp.Database;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp.Pages;

public partial class AuthenticationPage : Page {
    private AuthService authService;
    private FormInputMode currentMode;

    public AuthenticationPage() {
        InitializeComponent();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e) {
        var context = await AppDbContext.InitializeAsync();
        authService = new(context);
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

    private void SetInputFormVisibility(Label label, TextBox textbox, Visibility visibility) {
        label.Visibility = visibility;
        textbox.Visibility = visibility;
    }

    private async void SignInButton_Click(object sender, RoutedEventArgs e) {
        if (SwitchInputMode(FormInputMode.SignIn)) return; // change ui
        HintTextBlock.Text = "Authenticating...";
        string login = LoginTextBox.Text;
        string password = PasswordTextBox.Text;
        var ret = await authService.LoginAsync(login, password);
        if (ret.Success) {
            HintTextBlock.Text = "Auth success";
        } else {
            HintTextBlock.Text = $"Got error: {ret.Error}";
        }
    }

    private async void SignUpButton_Click(object sender, RoutedEventArgs e) {
        if (SwitchInputMode(FormInputMode.SignUp)) return; // change ui
        HintTextBlock.Text = "Registering...";
        string login = LoginTextBox.Text;
        string password = PasswordTextBox.Text;
        string email = EmailTextBox.Text;
        string phone = PhoneTextBox.Text;
        var ret = await authService.RegisterAsync(login, password, email, phone);
        if (ret.Success) {
            HintTextBlock.Text = "Registration successful";
        } else {
            HintTextBlock.Text = $"Got error: {ret.Error}";
        }


    }

    private enum FormInputMode {
        SignIn,
        SignUp,
    }

}


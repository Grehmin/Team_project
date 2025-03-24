using System.Windows;

namespace ShopApp.Utils;

public static class UiHelper {
    public static MainWindow GetMainWindow() => (Application.Current.MainWindow as MainWindow)!;
}

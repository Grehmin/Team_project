using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ShopApp.Utils;

public class ImagePathConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var imagePath = value as string;
        if (string.IsNullOrEmpty(imagePath)) {
            return "pack://application:,,,/Assets/default_product_image.jpeg";
        }
        imagePath = Paths.ToImage(imagePath);
        return new BitmapImage(new Uri(imagePath, UriKind.Absolute));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}

using System.IO;

namespace ShopApp.Utils;

public static class Paths {
    public static string MyDocuments => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    public static string AppDocuments => Path.Combine([MyDocuments, "ShopApp"]);
    public static string AppImages => Path.Combine([AppDocuments, "Images"]);
    public static string AppDatabase => Path.Combine([AppDocuments, "Database.sqlite"]);

    public static string ToImage(string image) => Path.Combine([AppImages, image]);

    // raises an exception if not recoverable
    public static void EnsureExists() {
        if (string.IsNullOrEmpty(MyDocuments) && !Directory.Exists(MyDocuments)) throw new Exception("Could not locate user's Documents directory.");
        Directory.CreateDirectory(AppDocuments);
        Directory.CreateDirectory(AppImages);
    }

}

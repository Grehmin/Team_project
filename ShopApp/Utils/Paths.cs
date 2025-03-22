using System.IO;

namespace ShopApp.Utils;

public static class Paths {
    public static string MyDocuments => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    public static string AppDocuments => Path.Combine([MyDocuments, "ShopApp"]);
    public static string AppDatabase => Path.Combine([AppDocuments, "database.sqlite"]);

    // raises an exception if not recoverable
    public static void EnsureExists() {
        if (string.IsNullOrEmpty(MyDocuments) && !Directory.Exists(MyDocuments)) throw new Exception("Could not locate user's Documents directory.");
        Directory.CreateDirectory(AppDocuments);
    }

}

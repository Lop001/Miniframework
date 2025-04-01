using System.Text;

namespace MiniFramework.Utils;

public static class CodeWriter
{
    public static void WriteToFile(string directory, string fileName, string code)
    {
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        string path = Path.Combine(directory, fileName);

        File.WriteAllText(path, code, Encoding.UTF8);
    }
}

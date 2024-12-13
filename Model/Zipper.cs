using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace passwrkk.Model;

public class Zipper
{
    public static void EncryptFolder(string enterPath, string outPath, string password)
    {
        using (FileStream fs = File.Create(outPath))
        using (ZipOutputStream zos = new ZipOutputStream(fs))
        {
            zos.Password = password;
            FileInfo info = new FileInfo(enterPath);
            ZipEntry entry = new(info.Name)
            {
                DateTime = info.LastWriteTime,
                Size = info.Length
            };
            zos.PutNextEntry(entry);

            using (FileStream fileStream = File.OpenRead(enterPath))
            {
                fileStream.CopyTo(zos);
            }
            zos.CloseEntry();
        }
    }
}


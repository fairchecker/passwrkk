using Microsoft.Win32;

namespace passwrkk.Model
{
    public static class ModelUtils
    {
        public static string GetFolderPath()
        {
            OpenFolderDialog dialog = new();
            if (dialog.ShowDialog() == true)
            {
                return dialog.FolderName;
            }
            throw new Exception("Error occured during getting path!");
        }

        public static string GetFilePath(string filter)
        {
            OpenFileDialog dialog = new();
            dialog.Filter = filter;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            throw new Exception("Error occured while getting file path!");
        }

        public static string GetVaultNameByPath(string path)
        {
            for (int i = path.Length - 6; i >= 0; i--)
            {
                if (path[i] == '\\')
                {
                    return path.Substring(i + 1, path.Length - 6 - i);
                }
            }
            return "ERROR";
        }
    }
}

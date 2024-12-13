using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

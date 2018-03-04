using System.IO;
using SyncExample.Droid.Services;
using SyncExample.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace SyncExample.Droid.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetDatabaseFilePath(string fileName)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
    }
}
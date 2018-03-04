using SyncExample.SQLite;
using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace SyncExample.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private ILocalDatabaseContext _context;

        public AboutViewModel(ILocalDatabaseContext context)
        {
            _context = context;
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}
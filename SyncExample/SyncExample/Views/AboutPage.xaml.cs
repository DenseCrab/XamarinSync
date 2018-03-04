using SyncExample.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SyncExample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ViewPage<AboutViewModel>
    {
        public AboutPage()
        {
            InitializeComponent();
        }
    }
}
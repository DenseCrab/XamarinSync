using System;
using System.ComponentModel;
using Autofac;
using Autofac.Core;
using SyncExample.Services;
using SyncExample.SQLite;
using SyncExample.Views;
using Xamarin.Forms;

namespace SyncExample
{
    public partial class App : Application
    {
        private static Syncing _syncing;
        private static Autofac.IContainer _container;


        public App(AppSetup setup)

        {
            AppContainer.Container = setup.CreateContainer();
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

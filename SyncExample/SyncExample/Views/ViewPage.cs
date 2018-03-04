using Autofac;
using SyncExample.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SyncExample.Views
{
    public class ViewPage<T> : ContentPage where T : IViewModel
    {
        readonly T _viewModel;
        public T ViewModel
        {
            get { return _viewModel; }
        }

        public ViewPage()
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                _viewModel = AppContainer.Container.Resolve<T>();
            }
            BindingContext = _viewModel;
        }
    }
}

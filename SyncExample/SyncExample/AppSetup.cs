using Autofac;
using Autofac.Core;
using SyncExample.API;
using SyncExample.Services;
using SyncExample.SQLite;
using SyncExample.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SyncExample
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);
            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder cb)
        {
            cb.RegisterType<Syncing>().SingleInstance();
            //cb.RegisterType<LocalDatabaseContext>().As<ILocalDatabaseContext>().SingleInstance();
            cb.RegisterType<APIContext>().As<IAPIContext>().SingleInstance();
            cb.Register(c => new LocalDatabaseContext(DependencyService.Get<IFileHelper>().GetDatabaseFilePath("SyncExample.db3"))).As<ILocalDatabaseContext>().SingleInstance();
            //cb.RegisterType<APIContext>().As<IAPIContext>().WithParameter(
            //     new ResolvedParameter(
            //       (pi, ctx) => pi.ParameterType == typeof(ILocalDatabaseContext),
            //       (pi, ctx) => ctx.ResolveKeyed<ILocalDatabaseContext>(pi.Name)));
            cb.RegisterType<AboutViewModel>().SingleInstance();
            cb.RegisterType<ItemsViewModel>().SingleInstance();
        }
    }

    public static class AppContainer
    {
        public static IContainer Container { get; set; }
    }
}

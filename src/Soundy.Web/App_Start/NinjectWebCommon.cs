using System.Web.Http;
using Soundy.Core.Mappers;
using Soundy.Core.Repositories;
using Soundy.Data.Context;
using Soundy.Data.Model;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Soundy.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Soundy.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Soundy.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }


        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<SoundyDb>().ToSelf().InSingletonScope();
            kernel.Bind<IRepository<Song>>().To<Repository<Song>>();
            kernel.Bind<IRepository<Playlist>>().To<Repository<Playlist>>();
            kernel.Bind<IRepository<Author>>().To<Repository<Author>>();
        }
    }
}

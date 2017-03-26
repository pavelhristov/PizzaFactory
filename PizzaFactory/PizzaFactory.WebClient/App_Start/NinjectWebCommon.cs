[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PizzaFactory.WebClient.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PizzaFactory.WebClient.App_Start.NinjectWebCommon), "Stop")]

namespace PizzaFactory.WebClient.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Service.Contracts;
    using Service;
    using Data;
    using Helpers.Contracts;
    using Helpers;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ICacheProvider>().To<CacheProvider>().InRequestScope();
            kernel.Bind<IPizzaService>().To<PizzaService>();
            kernel.Bind<IIngredientService>().To<IngredientService>();
            kernel.Bind<ICustomPizzaService>().To<CustomPizzaService>();
            kernel.Bind<IApplicationUserService>().To<ApplicationUserService>();
            kernel.Bind<IBaseDbContext, IPizzaFactoryDbContext, IIdentityDbContext, IOrderDbContext>()
                .To<PizzaFactoryDbContext>().InRequestScope();
        }
    }
}

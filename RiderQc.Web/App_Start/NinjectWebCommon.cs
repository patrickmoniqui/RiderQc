[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RiderQc.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(RiderQc.Web.App_Start.NinjectWebCommon), "Stop")]

namespace RiderQc.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using RiderQc.Web.Repository;
    using RiderQc.Web.DAL;
    using System.Web.Http;
    using Ninject.Web.WebApi;
    using RiderQc.Web.DAL.Interface;
    using RiderQc.Web.Repository.Interface;

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
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
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
            //daos
            kernel.Bind<IRideDao>().To<RideDao>();
            kernel.Bind<IUserDao>().To<UserDao>();
            kernel.Bind<IMotoDao>().To<MotoDao>();
            kernel.Bind<ILevelDao>().To<LevelDao>();
            kernel.Bind<ITrajetDao>().To<TrajetDao>();
            kernel.Bind<ICommentDao>().To<CommentDao>();
            kernel.Bind<IMessageDao>().To<MessageDao>();
            kernel.Bind<IUserRoleDao>().To<UserRoleDao>();
            kernel.Bind<IRatingDao>().To<RatingDao>();

            //repos
            kernel.Bind<IRideRepository>().To<RideRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IMotoRepository>().To<MotoRepository>();
            kernel.Bind<ILevelRepository>().To<LevelRepository>();
            kernel.Bind<ITrajetRepository>().To<TrajetRepository>();
            kernel.Bind<ICommentRepository>().To<CommentRepository>();
            kernel.Bind<IMessageRepository>().To<MessageRepository>();
            kernel.Bind<IUserRoleRepository>().To<UserRoleRepository>();
            kernel.Bind<IRatingRepository>().To<RatingRepository>();

        }        
    }
}

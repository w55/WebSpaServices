using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSpaServices.Models;
using Ninject.Extensions.Factory;
using Ninject.Web.Common;
using System.Data.Entity;

namespace WebSpaServices.Utils
{
    /// <summary>
    /// Класс NinjectDependencyResolver реализует интерфейс IDependencyResolver
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        /// <summary>
        /// Устанавливается сопоставление между интерфейсом-зависимостью и конкретным классом этого интерфейса
        /// </summary>
        private void AddBindings()
        {
            Trace.WriteLine("--- NinjectDependencyResolver.AddBindings() ---");

            AnimalsContext context = new AnimalsContext();

            kernel.Bind<AnimalsContext>().ToSelf().InRequestScope();

            kernel.Bind<IRepository<Skin>>().To<SkinRepository>()
                .WithConstructorArgument("context", context);

            kernel.Bind<IRepository<Kind>>().To<KindRepository>()
                .WithConstructorArgument("context", context);

            kernel.Bind<IRepository<Location>>().To<LocationRepository>()
                .WithConstructorArgument("context", context);

            kernel.Bind<IRepository<Region>>().To<RegionRepository>()
                .WithConstructorArgument("context", context);

            kernel.Bind<IRepository<Animal>>().To<AnimalRepository>()
                .WithConstructorArgument("context", context);

            kernel.Bind<IRepositoryFactory>().ToFactory();
        }
    }
}
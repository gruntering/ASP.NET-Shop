using Hub.Context;
using Hub.TablesRetriever;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Struct
{
    public class NinDependRes: IDependencyResolver
    {
        private IKernel kernel;

        public NinDependRes(IKernel kernelPar)
        {
            kernel = kernelPar;
            AddBindings();
        }

        private void AddBindings()
        {

            kernel.Bind<IMaterialRepository>().To<EFMaterialRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            kernel.Bind<IOrderProcessor>().To<EmailProcessor>()
                .WithConstructorArgument("settings", emailSettings);

        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);

        }
    }
}
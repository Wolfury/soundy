using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using Ninject;

namespace Soundy.Web.Common
{
    public class NinjectHttpDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectHttpDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            //Do nothing
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}

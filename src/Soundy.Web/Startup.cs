using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Soundy.Web.Startup))]

namespace Soundy.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}

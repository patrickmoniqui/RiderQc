using Ninject;
using RiderQc.Web.DAL;
using RiderQc.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RiderQc.Web
{
    public class NinjectBinding : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
        }

        public static void Setup()
        {
            StandardKernel _kernel = new StandardKernel();

            _kernel.Load(Assembly.GetExecutingAssembly());


        }
    }
}
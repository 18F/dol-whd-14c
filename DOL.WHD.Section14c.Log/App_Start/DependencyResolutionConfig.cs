﻿using DOL.WHD.Section14c.Log.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using SimpleInjector;
using System.Linq;
using System.Web;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace DOL.WHD.Section14c.Log
{
    /// <summary>
    /// Maps interfaces to concrete types
    /// </summary>
    public class DependencyResolutionConfig
    {
        /// <summary>
        /// Setup the mapping
        /// </summary>
        public static void Register()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);


            container.Register<IActivityLogRepository, ActivityLogRepository>(Lifestyle.Scoped);
            container.Register<IErrorLogRepository, ErrorLogRepository>(Lifestyle.Scoped);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
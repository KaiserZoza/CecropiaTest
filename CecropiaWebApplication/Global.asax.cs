using AutoMapper;
using CecropiaWebApplication.Models;
using Data;
using Entities;
using Logic;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CecropiaWebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<CecropiaTestContext,Configuration>());
            AutoMapperConfig.Initialize();
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<ISqlUnitOfWork, CecropiaTestContext>(Lifestyle.Scoped);
            container.Register<IPatientService, PatientService>();
            container.Register(typeof(IGenericRepository<Patient>),typeof(GenericSqlRepository<Patient>));
            container.Register(typeof(IGenericRepository<Country>), typeof(GenericSqlRepository<Country>));
            container.Register(typeof(IGenericRepository<BloodType>), typeof(GenericSqlRepository<BloodType>));
            container.Register(typeof(IGenericRepository<ExceptionRecord>), typeof(GenericSqlRepository<ExceptionRecord>));
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}

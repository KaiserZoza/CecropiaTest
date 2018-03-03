using AutoMapper;
using Entities;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using SimpleInjector.Integration.WebApi;
using Data;
using Logic;
using System.Data.Entity;

namespace CecropiaTest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer<CecropiaTestContext>(null);
            Mapper.Initialize(cfg => cfg.CreateMap<Exception, ExceptionRecord>()
            .ForMember(d => d.ExceptionRecordId, o => o.Ignore())
                  .ForMember(d => d.ExceptionRecordDate, o => o.Ignore())
                  .ForMember(d => d.ExceptionMessage, o => o.MapFrom(x => x.Message))
                  .ForMember(d => d.ExceptionStackTrace, o => o.MapFrom(x => x.StackTrace))
                  .ForMember(d => d.InnerExceptionMessage, o => o.MapFrom(x => x.InnerException.Message))
                  .ForMember(d => d.InnerExceptionStackTrace, o => o.MapFrom(x => x.InnerException.StackTrace))
                  .ForMember(d => d.ExceptionTypeName, o => o.MapFrom(x => x.GetType().FullName))
                  .ForMember(d => d.InnerExceptionSource, o => o.MapFrom(x => x.InnerException.Source)));
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            container.Register<ISqlUnitOfWork, CecropiaTestContext>(Lifestyle.Scoped);
            container.Register<IPatientService, PatientService>();
            container.Register(typeof(IGenericRepository<Patient>), typeof(GenericSqlRepository<Patient>));
            container.Register(typeof(IGenericRepository<Country>), typeof(GenericSqlRepository<Country>));
            container.Register(typeof(IGenericRepository<BloodType>), typeof(GenericSqlRepository<BloodType>));
            container.Register(typeof(IGenericRepository<ExceptionRecord>), typeof(GenericSqlRepository<ExceptionRecord>));
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}

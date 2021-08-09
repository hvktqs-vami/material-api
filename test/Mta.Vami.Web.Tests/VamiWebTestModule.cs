using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Mta.Vami.EntityFrameworkCore;
using Mta.Vami.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Mta.Vami.Web.Tests
{
    [DependsOn(
        typeof(VamiWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class VamiWebTestModule : AbpModule
    {
        public VamiWebTestModule(VamiEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(VamiWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(VamiWebMvcModule).Assembly);
        }
    }
}
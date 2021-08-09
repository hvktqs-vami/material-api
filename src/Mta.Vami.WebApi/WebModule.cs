using Microsoft.Extensions.DependencyInjection;
using Mta.Vami.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    public static class WebModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(WorkingContext<>));
            services.AddTransient<CountryService>();
            services.AddTransient<MaterialChemicalService>();
            services.AddTransient<MaterialContentService>();
            services.AddTransient<MaterialEquivalentService>();
            services.AddTransient<MaterialGroupService>();
            services.AddTransient<MaterialHighFatigueService>();
            services.AddTransient<MaterialHighTempMecPropService>();
            services.AddTransient<MaterialChemicalTypeService>();
            services.AddTransient<MaterialLowFatigueService>();
            services.AddTransient<MaterialService>();
            services.AddTransient<MaterialUsageService>();
            services.AddTransient<MaterialStandardService>();
            services.AddTransient<MaterialSubGroupService>();
            services.AddTransient<MaterialMechanicalPropGroupService>();
            services.AddTransient<MaterialMechanicalPropService>();
            services.AddTransient<MaterialMechanicalPropTypeService>();
        }
    }
}

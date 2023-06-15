using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.StudentCRUD.Data.Mapping;
using Nop.Plugin.Widgets.StudentCRUD.Factory;
using Nop.Plugin.Widgets.StudentCRUD.Service;

namespace Nop.Plugin.Widgets.StudentCRUD.Infrastructure
{
    // dependency injection er kaaj 
    public class NopStartup : INopStartup
    {
        public int Order => 100;

        public void Configure(IApplicationBuilder application)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentModelFactory, StudentModelFactory>();
            services.AddAutoMapper(typeof(StudentAutomapperConfig).Assembly);
        }
    }
}

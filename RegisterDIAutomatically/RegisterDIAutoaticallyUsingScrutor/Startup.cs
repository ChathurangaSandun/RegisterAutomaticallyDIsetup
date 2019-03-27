using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceProject.Concreates;
using ServiceProject.Interfaces;
using System.Linq;

namespace RegisterDIAutoaticallyUsingScrutor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IServicebase, Servicebase>();
            services.Scan(scan => scan
  // We start out with all types in the assembly of IAssemblyMarker
  .FromAssemblyOf<IServicebase>()
  // AddClasses starts out with all public, non-abstract types in this assembly.
  // These types are then filtered by the delegate passed to the method.
  // In this case, we filter out only the classes that are assignable to IRepository.
  .AddClasses(classes => classes.AssignableTo<IServicebase>())
  // We then specify what type we want to register these classes as.
  // In this case, we want to register the types as all of its implemented interfaces.
  // So if a type implements 3 interfaces; A, B, C, we'd end up with three separate registrations.
  .AsImplementedInterfaces()
  // And lastly, we specify the lifetime of these registrations.
  .WithTransientLifetime());
 

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

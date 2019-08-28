using Autofac;
using Autofac.Extensions.DependencyInjection;
using LiteRepository.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryCore.Interfaces;
using Service.Db;
using System;
using System.Reflection;

namespace CoreServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            ContainerBuilder builder = new ContainerBuilder();
            services.AddCors();
            Assembly assembly = typeof(IRepositoryCore<,>).Assembly;
            //builder.RegisterGeneric(typeof(IRepositoryCore<,>), assembly);
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // Assembly[] assemblies = GetYourAssemblies();
            builder.RegisterGeneric(typeof(LiteRepository.Repository.LiteRepository<>))
        .As(typeof(IRepositoryCore<,>))
        .InstancePerLifetimeScope();
           var lite= new  LiteDbContext();
            builder.RegisterInstance(lite).As<ILiteContext>();
            builder.Populate(services);
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder
               .WithOrigins("*")
               .AllowAnyMethod()
               .AllowAnyHeader());
            app.UseMvc();
        }
    }
}

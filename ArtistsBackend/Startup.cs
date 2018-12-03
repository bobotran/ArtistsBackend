using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtistsBackend.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ArtistsBackend
{
    public class Startup
    {
        public static Action<IMapperConfigurationExpression> MapperInit()
        {
            return (cfg) => {
                cfg.CreateMap<Entities.User, Models.UserDto>();
                cfg.CreateMap<Entities.Performance, Models.PerformanceDto>();
                cfg.CreateMap<Entities.Address, Models.AddressDto>();
                cfg.CreateMap<Models.AddressDto, Entities.Address>();
                cfg.CreateMap<Entities.Event, Models.EventDto>();
                cfg.CreateMap<Entities.Event, Models.EventForUpdateDto>();
                cfg.CreateMap<Models.EventForUpdateDto, Entities.Event>();
                cfg.CreateMap<Models.PerformanceForCreationDto, Entities.Performance>();
                cfg.CreateMap<Entities.Performance, Models.PerformanceForCreationDto>();
            };
        }

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                services.AddDbContext<ArtistsContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));
            else
            {
                string connectionString = Configuration.GetValue<string>("connectionStrings:ArtistsBackendConnectionString");
                services.AddDbContext<ArtistsContext>(o => o.UseSqlServer(connectionString));
            }
            services.AddScoped<IArtistsRepository, ArtistsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ArtistsContext artistsContext,
            IArtistsRepository repository)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            artistsContext.EnsureSeedDataForContext();
            //app.UseHttpsRedirection();
            //Mapper.Initialize(cfg => MapperInit()(cfg));
            app.UseMvc();
        }
    }
}

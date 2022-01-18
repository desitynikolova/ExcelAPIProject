using AppServer.ApiServices;
using Date;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Services.Implementation;
using Services.Interfaces;
using Services.MapperSettings;
using Services.ModelServices.Implementation;
using Services.ModelServices.Interfaces;
using Services.ViewModels;
using System;
using System.Linq;
using System.Text;

namespace AppServer
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
            services.Configure<QuartzOptions>(Configuration.GetSection("Quartz"));
            services.Configure<QuartzOptions>(options =>
            {
                options.Scheduling.IgnoreDuplicates = true; 
                options.Scheduling.OverWriteExistingData = true; 
            });
            services.AddQuartz(q =>
            {
                q.SchedulerId = "Scheduler-Core";

                q.UseMicrosoftDependencyInjectionJobFactory();

                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 10;
                });
                q.ScheduleJob<SchedFolderReaderJob>(trigger => trigger
                    .WithIdentity("Combined Configuration Trigger")
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(7)))
                    .WithDailyTimeIntervalSchedule(x => x.WithInterval(5, IntervalUnit.Minute))
                    .WithDescription("trigger was called")
                );
                q.AddJob<SchedFolderReaderJob>(j => j
                    .StoreDurably()
                    .WithDescription("run job")
                );

            });
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            services.AddControllers();
            services.AddAutoMapper(typeof(Profiles));
            services.AddScoped<IIdentityUser, IdentityUserService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<ITransfer, Transfer>();
            services.AddScoped<IFilesReader, FilesReader>();
            services.AddScoped<IDatabaseTransfer,DatabaseTransfer>();


            services.AddDbContext<ApplicationDbContext>();
            services.Configure<TokenModel>(Configuration.GetSection("tokenManagement"));
            var token = Configuration.GetSection("tokenManagement").Get<TokenModel>();
            var secret = Encoding.ASCII.GetBytes(token.Secret);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                        ValidIssuer = token.Issuer,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppServer", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppServer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.Use((req,res)=>
                {
                    var headers = req.Request.Headers.ToList();
                    return res.Invoke();
                }); 

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

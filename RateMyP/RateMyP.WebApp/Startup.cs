using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RateMyP.WebApp.Statistics;

namespace RateMyP.WebApp
    {
    public class Startup
        {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            {
            Configuration = configuration;
            }

        public void ConfigureServices(IServiceCollection services)
            {
            services.AddDbContext<RateMyPDbContext>(opt => opt.UseSqlServer(Configuration["ConnectionString:RateMyPDB"]));
            services.AddTransient<ICourseStatisticsAnalyzer, CourseStatisticsAnalyzer>();
            services.AddTransient<ITeacherStatisticsAnalyzer, TeacherStatisticsAnalyzer>();
            services.AddTransient<ILeaderboardManager, LeaderboardManager>();

            services.AddControllersWithViews();
            services.AddMvc()
                    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });

            services.AddSwaggerGen(c =>
                                       {
                                           c.SwaggerDoc("v1", new OpenApiInfo { Title = "RateMyP app", Version = "v1" });
                                       });
            services.AddHttpClient();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                                  {
                                      options.Authority = "https://securetoken.google.com/ratemyp-44d4c";
                                      options.TokenValidationParameters = new TokenValidationParameters
                                          {
                                          ValidateIssuer = true,
                                          ValidIssuer = "https://securetoken.google.com/ratemyp-44d4c",
                                          ValidateAudience = true,
                                          ValidAudience = "ratemyp-44d4c",
                                          ValidateLifetime = true
                                          };
                                  });
            }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
                }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                    spa.UseReactDevelopmentServer("start");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                                 {
                                     c.SwaggerEndpoint("/swagger/v1/swagger.json", "RateMyP V1");
                                 });
            }
        }
    }

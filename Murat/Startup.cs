using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Murat.BusinessLogic.Helpers;
using Murat.DataAccess;
using Murat.BusinessLogic.Interfaces;
using Murat.BusinessLogic.Implementations;
using Murat.UnitOfWork;

namespace Murat.WebApi
{
    public class Startup
    {
        private readonly string MyAllowedOrigin = "_MyOriginPolicy";
        private readonly string MyAllowedOrigin2 = "_MyOriginPolicy2";
        private readonly string LocalOrigin = "_LocalOrigin";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            #region ENABLE CORS
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowedOrigin,
                    builder =>
                    {
                        //builder.WithOrigins(Configuration["AppSettings:AllowedOrigins"].Split(";"))
                        builder.WithOrigins((Configuration.GetSection("AppSettings").GetSection("Origins").Value).Split(";"))
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });

                options.AddPolicy(MyAllowedOrigin2,
                    builder =>
                    {
                        builder.WithOrigins((Configuration.GetSection("AppSettings").GetSection("Origins2").Value).Split(";"))
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });

                options.AddPolicy(LocalOrigin,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            #endregion

            #region SECURITY SERVICES
            services.AddTransient<ISecurityLogic, SecurityLogic>();
            #endregion

            #region MAINTENCE SERVICES
            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<ITablaMaestraLogic, TablaMaestraLogic>();
            services.AddTransient<IComboLogic, ComboLogic>();
            services.AddTransient<IProductoLogic, ProductoLogic>();
            services.AddTransient<IMarcaLogic, MarcaLogic>();
            services.AddTransient<ISliderLogic, SliderLogic>();
            services.AddTransient<ICommonLogic, CommonLogic>();
            services.AddTransient<IPublicadoLogic, PublicadoLogic>();
            #endregion

            #region STORE SERVICES
            services.AddTransient<IMuratServicesLogic, MuratServicesLogic>();
            #endregion

            #region SQL CONNECTION
            services.AddSingleton<IUnitOfWork>(option => new ProjectUnitOfWork(
            Configuration.GetConnectionString("Project"), Configuration
            ));
            #endregion

            #region OAUTH 2.0
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            });
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvcCore().AddAuthorization();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();
            app.UseHttpsRedirection();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHsts();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

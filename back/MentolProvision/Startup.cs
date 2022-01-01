using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using NSwag;
using NSwag.Generation.Processors.Security;
using MentolProvision.Auth;
using MentolProvisionModel;
using MentolProvisionLdapAuth;
using MentolProvisionInterface;
using MentolProvisionRepository;
using MentolProvisionData.Config;
using MentolProvisionLdapAuth.Model;
using MentolProvision.Auth.Jwt;
using MentolProvision.Auth.Identity;
using MentolProvision.JsonConverters;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace MentolProvision
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        private const string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";
        private readonly DatabaseSettings _dbConfiguration;
        private readonly CultureInfo[] _supportedCultures = new[]
            {
                new CultureInfo("ru")
            };
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _dbConfiguration = Configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	        services.AddSingleton(x => Log.Logger);
	        services.AddDbContext<ApplicationContext>(options =>
	            {
		            options.UseSqlServer(_dbConfiguration.ConnectionString,
				            sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); })
			            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug())); // For EF Debug

	            })
                .AddTransient<TokenManagerMiddleware>()
                .Configure<LdapConfig>(Configuration.GetSection(nameof(LdapConfig)))
                .AddLdapAuthService();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher>()
                    .AddTransient<IUserStore<User>, UserStore>()
                    .AddTransient<IRoleStore<Role>, RoleStore>()
                    .AddIdentity<User, Role>()
                    .AddUserStore<UserStore>()
                    .AddRoleStore<RoleStore>()
                    .AddDefaultTokenProviders();

            services.AddSingleton<IDataInterface, Repository>(sp =>
            {
                var scope = sp.CreateScope();
                var dbContext = scope.ServiceProvider.GetService<ApplicationContext>();
                return new Repository(dbContext);
            });

            services.AddSwaggerDocument(config =>
            {
                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));
                config.AddSecurity("JWT token", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Copy 'Bearer ' + valid JWT token into field",
                    In = OpenApiSecurityApiKeyLocation.Header
                });
                config.PostProcess = (document) =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "MyRest-API";
                    document.Info.Description = "ASP.NET Core 3.1 MyRest-API";
                };
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // укзывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = true,
                    // строка, представляющая издателя
                    ValidIssuer = AuthOptions.ISSUER,
                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = AuthOptions.AUDIENCE,
                    // будет ли валидироваться время существования
                    ValidateLifetime = true,
                    // установка ключа безопасности
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
                /* options.AddPolicy(Roles.Admin.ToString("f"), builder =>
                 {
                     builder.RequireClaim(ClaimTypes.Role, Roles.Admin.ToString("f"));
                 });
                */
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: _myAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("*");
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyHeader();
                                  });
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new NullableDateTimeConverter());
            });
                        
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "wwwroot");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture("ru"),
                    SupportedCultures = _supportedCultures,
                    SupportedUICultures = _supportedCultures
                })
               .UseRouting()
               .UseCors(_myAllowSpecificOrigins)             
               .UseStaticFiles()
               .UseAuthorization()
               .UseMiddleware<TokenManagerMiddleware>()
               .UseOpenApi()
               .UseSwaggerUi3()               
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllerRoute(
                       name: "completed",
                       pattern: "tasks/completed/{action=Index}/{id?}",
                       defaults: new { controller = "CompletedTasks" });
                   endpoints.MapControllerRoute(
                       name: "modelsList",
                       pattern: "modelsList/{action=Index}/{id?}",
                       defaults: new { controller = "VendorModels" });
                   endpoints.MapControllerRoute(
                       name: "rolesList",
                       pattern: "rolesList/{action=Index}/{id?}",
                       defaults: new { controller = "Roles" });
                   endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller}/{action=Index}/{id?}");
                });

            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
            });
        }
    }
}
using AgAmuasiBooking.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;

namespace AgAmuasiBooking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register an Npgsql data source for use by Npgsql
            builder.Services.AddNpgsqlDataSource(builder.Configuration.GetConnectionString("DefaultConnection")!, x =>
            {
                x.EnableDynamicJson()
                 .EnableParameterLogging(builder.Environment.IsDevelopment());
            });

            builder.Services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), opt =>
                {
                    opt.UseRelationalNulls(true)
                       .EnableRetryOnFailure(3)
                       .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }).UseLowerCaseNamingConvention()
                .EnableDetailedErrors(builder.Environment.IsDevelopment())
                .EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
            });

            builder.Services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = builder.Configuration.GetConnectionString("Valkey");
                o.InstanceName = "booking_";
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(x =>
            {
                x.SignIn.RequireConfirmedAccount = false;
                x.Password.RequiredLength = 8;
                x.Password.RequireNonAlphanumeric = true;
                x.Password.RequireDigit = true;
                x.Password.RequireUppercase = true;
                x.Password.RequireLowercase = true;
                x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddSingleton<IAppFeatures, AppFeatures>();

            // Get JWT settings directly from configuration
            var jwtSettings = builder.Configuration.GetSection("AppFeatures").Get<AppModel>() ?? throw new InvalidOperationException("AppFeatures configuration section is missing");
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ValidateIssuer = true,
                    RequireExpirationTime = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience
                };
            });

            // Add Authorization Policies
            builder.Services.AddAuthorizationBuilder()
                // Add Authorization Policies
                .AddPolicy("Users", policy => policy.RequireRole("User"))
                // Add Authorization Policies
                .AddPolicy("Administration", policy => policy.RequireRole(["Administrator", "Developer"]))
                // Add Authorization Policies
                .AddPolicy("Manager", policy =>
                    policy.RequireRole(["Manager", "Administrator", "Developer"])
                    )
                .AddDefaultPolicy("Default", x => x.RequireAuthenticatedUser());

            // Health checks - provide connection string for Npgsql
            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!)
                .AddRedis(builder.Configuration.GetConnectionString("Valkey")!);

            builder.Services.AddDataProtection();
            builder.Services.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN");

            // Fix: correct C# array initializer syntax
            string[] locs = builder.Environment.IsDevelopment()
                ? ["http://localhost:4200"]
                : ["https://bookings.ghid-kccr.org"];

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("bStudioApps",
                    x => x.WithOrigins(locs)
                          .WithHeaders("Content-Type", "Accept", "Origin", "Authorization", "X-XSRF-TOKEN", "XSRF-TOKEN", "enctype", "Access-Control-Allow-Origin", "Access-Control-Allow-Credentials", "patient-id", "info")
                          .WithMethods("GET", "POST", "OPTIONS", "PUT", "DELETE")
                          .AllowCredentials());
            });

            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR(x => x.KeepAliveInterval = TimeSpan.FromSeconds(10));
            builder.Services.AddResponseCaching();

            // Register IHttpContextAccessor before using it
            builder.Services.AddHttpContextAccessor();

            // Expose CancellationToken for the current HTTP request safely
            builder.Services.AddScoped(typeof(CancellationToken), sp =>
            {
                var ctx = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                return ctx?.RequestAborted ?? CancellationToken.None;
            });

            // Add OpenAPI and Swagger services (Development only)
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddOpenApi();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "AgAmuasi Booking API",
                        Description = "API for managing bookings and appointments",
                        Contact = new OpenApiContact
                        {
                            Name = "Global Health and Infectious Disease Group",
                            Email = "ghid@kccr.de"
                        }
                    });

                    // Add JWT Authentication to Swagger
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                });
            }

            var app = builder.Build();

            // Forwarded headers first (before other proxy-dependent middleware)
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Routing must be set before authentication/authorization
            app.UseRouting();

            // Use CORS with correct policy name
            app.UseCors("bStudioApps");

            // Authentication & Authorization in correct order
            app.UseAuthentication();
            app.UseAuthorization();

            // Response caching should be enabled before endpoints are executed
            app.UseResponseCaching();

            // Map endpoints
            app.MapControllers();

            // Enable OpenAPI and Swagger UI (Development only)
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "AgAmuasi Booking API v1");
                    options.RoutePrefix = "swagger";
                    options.DocumentTitle = "AgAmuasi Booking API Documentation";
                    options.DisplayRequestDuration();
                });
            }

            app.Run();
        }
    }
}

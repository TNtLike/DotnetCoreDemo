using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
namespace MyWebApi
{
    public class Startup
    {

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TokenManagement>(Configuration.GetSection("TokenManagement"));
            services.AddSingleton<TokenManagement>(sp => sp.GetRequiredService<IOptions<TokenManagement>>().Value);
            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBSettings"));
            services.AddSingleton<MongoDBSettings>(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["TokenManagement:secret"])),
                    ValidIssuer = Configuration["TokenManagement:issuer"],
                    ValidAudience = Configuration["TokenManagement:audience"],
                    ValidateIssuer = false,
                    ValidateAudience = false,

                };
            });
            services.AddSingleton<CodeService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<JobService>();
            services.AddSingleton<FileService>();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://127.0.0.1:5500");
                                  });
            });
            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/ws", SocketHandler.Map);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            // Authentication是认证，明确是你谁，确认是不是合法用户。常用的认证方式有用户名密码认证。
            // Authorization是授权，明确你是否有某个权限。当用户需要使用某个功能的时候，系统需要校验用户是否需要这个功能的权限。

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}

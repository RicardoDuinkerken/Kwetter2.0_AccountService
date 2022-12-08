using AccountService.Api.Controllers;
using AccountService.Core.Services.Interfaces;
using AccountService.Dal.Context;
using AccountService.Dal.Models;
using GenericDal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AccountService.Api;

    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //database connection
            string server = Configuration["Server"] ?? "localhost";
            string port = Configuration["Port"] ?? "3306";
            string username = Configuration["Username"] ?? "root";
            string password = Configuration["Password"] ?? "Geheim_101";
            string database = Configuration["Database"] ?? "Account";
            string connectionString = $"server={server};Port={port};user={username};password={password};database={database}";

            services.AddDbContext<AccountServiceContext>(builder =>
                builder.UseMySQL(connectionString));

            //repositories
            services
                .AddTransient<IAsyncRepository<Account, long>,
                    BaseAsyncRepository<Account, long, AccountServiceContext>>();

            //services  
            services.AddTransient<IAccountService, Core.Services.AccountService>();

            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AccountServiceContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            context.Database.EnsureCreated();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<AccountController>();

                endpoints.MapGet("/",
                    async context =>
                    {
                        await context.Response.WriteAsync(
                            "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                    });
            });
        }
    }
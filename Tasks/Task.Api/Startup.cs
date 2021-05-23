using Core.Repositories.IRepository;
using Core.Services.IService;
using Ideal.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Task.DataAccess;
using Task.Repositories;
using Task.Services;

namespace Task.Api
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
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Task.Api", Version = "v1"}); });
            
            services.AddDbContext<TaskDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SQLServer"),
                    x => x.MigrationsAssembly("Task.DataAccess")));

            services.AddScoped<DbContext, TaskDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // services.AddScoped<IEmployeeReceiverService, EmployeeReceiverService>();
            services.AddHostedService<EmployeeReceiverService>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            InitializeDatabase(app);
        }
        
        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<TaskDbContext>())
                {
                    if (context.Database.CanConnect())
                    {
                        context.Database.Migrate();
                    }
                    else
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                    }

                }
            }
        }
    }
}
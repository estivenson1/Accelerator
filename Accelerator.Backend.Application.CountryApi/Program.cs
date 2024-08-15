
using Accelerator.Backend.Business;
using Accelerator.Backend.Contracts.Business;
using Accelerator.Backend.Contracts.ExternalServices;
using Accelerator.Backend.Entities._1Referentials;
using Accelerator.Backend.ExternalServices;

namespace Accelerator.Backend.Application.CountryApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            #region DependencySettings
            builder.Services.Configure<AppSettings>(opt => builder.Configuration.GetSection("AppSettings").Bind(opt));
            builder.Services.Configure<List<ServiceSettings>>(opt => builder.Configuration.GetSection("ServiceSettings").Bind(opt));
            #endregion

            #region DependencyExternalServices
            builder.Services.AddTransient<ICountryExternalService, CountryExternalService>();
            #endregion

            #region DependencyBusiness   
            builder.Services.AddTransient<ICountryBL, CountryBL>();
            #endregion


            var app = builder.Build();
            //app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

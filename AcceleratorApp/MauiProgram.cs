using Accelerator.Frontend.Business;
using Accelerator.Frontend.Contracts.Business;
using Accelerator.Frontend.Contracts.ExternalServices;
using Accelerator.Frontend.ExternalServices;
using Accelerator.Frontend.Utils;
using AcceleratorApp.ViewModels;
using AcceleratorApp.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;
using System.Reflection;

namespace AcceleratorApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            var assemblyInfoJson = Assembly.GetExecutingAssembly();
            using var stream = assemblyInfoJson.GetManifestResourceStream("AcceleratorApp.appsettings.json");

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();


            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif



            #region DependencySettings
            builder.Configuration.GetSection(nameof(ConfigurationBind)).Bind(new ConfigurationBind());
            //builder.Services.Configure<AppSettings>(opt => builder.Configuration.GetSection("AppSettings").Bind(opt));
            //builder.Services.Configure<List<ServiceSettings>>(opt => builder.Configuration.GetSection("ServiceSettings").Bind(opt));
            #endregion

            #region DependencyExternalServices
            builder.Services.AddTransient<ICountryExternalService, CountryExternalService>();
            #endregion

            #region DependencyBusiness   
            builder.Services.AddTransient<ICountryBL, CountryBL>();
            #endregion

            #region DependencyViewsAndViewModels
            builder.Services.AddTransientWithShellRoute<CountriesView, CountriesViewModel>(nameof(CountriesView));
            #endregion


            return builder.Build();
        }
    }
}

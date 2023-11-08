using Microsoft.Extensions.Logging;
using RegistroPersonas.DataAccess;
using RegistroPersonas.ViewModels;
using RegistroPersonas.Views;

namespace RegistroPersonas
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var dbContext = new PersonaDbContext();
            dbContext.Database.EnsureCreated(); 
            dbContext.Dispose();

            builder.Services.AddDbContext<PersonaDbContext>();
            builder.Services.AddTransient<PersonaPage>();
            builder.Services.AddTransient<PersonasViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            Routing.RegisterRoute(nameof(PersonaPage), typeof(PersonaPage));
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
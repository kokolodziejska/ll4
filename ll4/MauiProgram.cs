using ll4;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Net.Http;



namespace FilmDiary;

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
            });

        builder.Services.AddSingleton<IFilmService, FilmService>();
        builder.Services.AddTransient<FilmsViewModel>();

        builder.Services.AddTransient<MainPage>(serviceProvider =>
        {
            var viewModel = serviceProvider.GetRequiredService<FilmsViewModel>();
            return new MainPage(viewModel);
        });

        builder.Services.AddLogging(logging =>
        {
            logging.AddConsole();
            logging.AddDebug();
        });



        builder.Services.AddHttpClient<IFilmService, FilmService>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5018/api/");
        });

        return builder.Build();
    }
}
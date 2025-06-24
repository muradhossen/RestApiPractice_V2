

using Microsoft.Extensions.DependencyInjection;
using Movies.Api.Sdk;
using Movies.Api.Sdk.Consumer;
using Movies.Contracts.Requests;
using Refit;
using System.Text.Json;

//var moviesApi = RestService.For<IMoviesApi>("https://localhost:7196");



var services = new ServiceCollection();



services
    .AddSingleton<AuthTokenProvider>()
    .AddTransient<AuthHeaderHandler>() // Register handler
    .AddRefitClient<IMoviesApi>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5001"))
        .AddHttpMessageHandler<AuthHeaderHandler>()
    .ConfigureHttpClient(x =>
    {
        x.BaseAddress = new Uri("https://localhost:5001");
    });

var provider = services.BuildServiceProvider();

var moviesApi = provider.GetRequiredService<IMoviesApi>();

var movie = await moviesApi.GetMovieAsync("a69786f2-1f2d-40bc-8d84-d5b77bdfd35b");

//var movies = await moviesApi.GetMoviesAsync(new GetAllMoviesRequest { Page = 1, PageSize = 3 });

//foreach (var item in movies.Items)
//{
//    Console.WriteLine(JsonSerializer.Serialize(item));
//}

Console.ReadLine();

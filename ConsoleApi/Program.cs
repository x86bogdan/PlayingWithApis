using ClassLibrary1;
using Microsoft.Extensions.Configuration;
using MyAPICode;

IConfiguration config = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddUserSecrets<Program>()
        .Build();

//await UseDogApi();
UseNutritionApi();
Console.ReadKey();

void UseNutritionApi()
{
    var api = new NutritionAPI(config);
    var result = api.GetNutritionData();
    Console.WriteLine(result);
}

async Task UseDogApi()
{
    var api = new DogApi();
    for (int i = 0; i < 10; i++)
    {
        var result = await api.GetRandomDogAsync("african");
        Console.WriteLine(result.Status);
        Console.WriteLine(result.Message);
    }
}
using System.Diagnostics;
using ClassLibrary1;
using Microsoft.Extensions.Configuration;
using MyAPICode;

IConfiguration config = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddUserSecrets<Program>()
        .Build();

await UseStudentMockApi();

//await UseDogApi();
//UseNutritionApi();
Console.ReadKey();

void UseNutritionApi()
{
    var api = new NutritionAPI(config);
    var result = api.GetNutritionData();
    Console.WriteLine(result);
}

async Task UseDogApi()
{
    var timer = new Stopwatch();
    var api = new DogApi();
    for (int i = 0; i < 10; i++)
    {
        timer.Start();
        var result = await api.GetRandomDogAsync("african");
        timer.Stop();
        Console.Write($"Normal API: Execution: {result.Status} Elapsed miliseconds: {timer.ElapsedMilliseconds} ");
        Console.WriteLine(result.Message);
        timer.Reset();
    }

    var limitedApi = new DogApiLimited();
    for (int i = 0; i < 10; i++)
    {
        timer.Start();
        var result = await limitedApi.GetRandomDogAsync("african");
        timer.Stop();
        Console.Write($"Limited API: Execution: {result.Status} Elapsed miliseconds: {timer.ElapsedMilliseconds} ");
        Console.WriteLine(result.Message);
        timer.Reset();
    }
}

static async Task UseStudentMockApi()
{
    var api = new StudentMockApi();
    var result = await api.GetStudentData();
    Console.WriteLine(result);
}
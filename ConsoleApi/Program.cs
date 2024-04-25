using ClassLibrary1;
using MyAPICode;

//await UseDogApi();
UseNutritionApi();
Console.ReadKey();

static void UseNutritionApi()
{
    var api = new NutritionAPI();
    var result = api.GetNutritionData();
    Console.WriteLine(result);
}

static async Task UseDogApi()
{
    var api = new DogApi();
    for (int i = 0; i < 10; i++)
    {
        var result = await api.GetRandomDogAsync("african");
        Console.WriteLine(result.Status);
        Console.WriteLine(result.Message);
    }
}
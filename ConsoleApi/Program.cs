using ClassLibrary1;

var api = new DogApi();
for (int i = 0; i < 10; i++)
{
    var result = await api.GetRandomDogAsync("african");
    Console.WriteLine(result.Status);
    Console.WriteLine(result.Message);
}
Console.ReadKey();
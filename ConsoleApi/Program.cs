using ClassLibrary1;

var api = new DogApi();
var result = await api.GetRandomDogAsync("african");
Console.WriteLine(result);
Console.WriteLine("****");
Console.WriteLine(result.Message);
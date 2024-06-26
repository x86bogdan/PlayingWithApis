﻿using System.Diagnostics;
using ClassLibrary1;
using Microsoft.Extensions.Configuration;
using MyAPICode;

IConfiguration config = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddUserSecrets<Program>()
        .Build();


UseHumanAndDog(config);
//await UseStudentMockApi();
//await UseDogApi(config);
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

static async Task UseStudentMockApi(IConfiguration configuration)
{
    var api = new StudentMockApi(configuration);
    var students = await api.GetStudentData();
    foreach (var student in students)
    {
        Console.WriteLine($"Informatii student: {student.FirstName} {student.LastName}");
        Console.WriteLine($"Id: {student.Id}, DataNasterii:, {student.DateOfBirth}, Adresa: {student.Address?.Street} {student.Address?.Number}, {student.Address?.City}");
    }
}

static void UseHumanAndDog(IConfiguration configuration)
{
    var api = new HumanAndDog();
    var result = api.GetStudentAndDog(configuration);
    foreach (var student in result)
    {
        Console.WriteLine($"Informatii student: {student.FirstName} {student.LastName}, Rasa caine: {student.DogBreed}, poza: {student.DogImage}");
    }
}
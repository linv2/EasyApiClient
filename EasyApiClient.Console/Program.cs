// See https://aka.ms/new-console-template for more information

using EasyApiClient;
using EasyApiClient.Console;

var httpBin = Http.Create<IHttpBin>();

Console.WriteLine(httpBin.TestGet(1, "A"));
Console.WriteLine("--------------------------");

Console.WriteLine(httpBin.TestGet(2, "b"));
Console.WriteLine("--------------------------");


Console.WriteLine(httpBin.TestPost(3, "c","param1","param2"));
Console.WriteLine("--------------------------");
Console.ReadKey();

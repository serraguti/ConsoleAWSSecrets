using ConsoleAWSSecrets;
using ConsoleAWSSecrets.Helpers;
using ConsoleAWSSecrets.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

Console.WriteLine("Ejemplo secrets manager");
string miSecreto = await HelperSecretManager.GetSecretAsync();
Console.WriteLine(miSecreto);

//PODEMOS DAR FORMATO A NUESTRO SECRET PARA PODER UTILIZARLO COMO CLASE
KeysModel model = JsonConvert.DeserializeObject<KeysModel>(miSecreto);
Console.WriteLine("MySql: " + model.Mysql);


//ALMACENAMOS EL OBJETO KEYSMODEL DENTRO DE DY
//ESTO LO HARIAMOS EN CUALQUIER APP API O MVC EN Program
var provider =
    new ServiceCollection()
    .AddTransient<ClaseTest>()
    .AddSingleton<KeysModel>(x => model)
    .BuildServiceProvider();

//EN CUALQUIER CLASE PODEMOS RECUPERAR LAS KEYS, POR EJEMPLO, 
//EN EL PROPIO PROGRAM SI NECESITAMOS CONNECTIONSTRING O EN 
//CUALQUIER OTRA CLASE DE INYECCION
var service = provider.GetService<KeysModel>();
string connectionString = service.Mysql;
Console.WriteLine(connectionString);

var test = provider.GetService<ClaseTest>();
Console.WriteLine("Api: " + test.GetValue());

using System.Net.Http;
using APIWalkthrough.Models;
using Newtonsoft.Json;
using APIWalkthrough;



HttpClient httpClient = new HttpClient();

HttpResponseMessage response = httpClient.GetAsync("https://swapi.dev/api/people/1").Result;

if (response.IsSuccessStatusCode)
{
    // var content = response.Content.ReadAsStringAsync().Result;
    // var person = JsonConvert.DeserializeObject<Person>(content);

    Person luke = response.Content.ReadAsAsync<Person>().Result;
    Console.WriteLine(luke.Name);

    foreach (string vehicleUrl in luke.Vehicles)
    {
        HttpResponseMessage vehicleResponse = httpClient.GetAsync(vehicleUrl).Result;
        // Console.WriteLine(vehicleResponse.Content.ReadAsStringAsync().Result);

        Vehicle vehicle = vehicleResponse.Content.ReadAsAsync<Vehicle>().Result;
        Console.WriteLine(vehicle.Name);
    }
}
System.Console.WriteLine();

SWAPIService service = new SWAPIService();
Person person = service.GetPersonAsync("https://swapi.dev/api/people/11").Result;
if (person != null)
{
    Console.WriteLine(person.Name);

    foreach (var vehicleUrl in person.Vehicles)
    {
        var vehicle = service.GetVehicleAsync(vehicleUrl).Result;
        Console.WriteLine(vehicle.Name);
    }
}
System.Console.WriteLine();

// this can take in any class we have created, not just Vehicle
var genericResponse = service.GetAsync<Vehicle>("https://swapi.dev/api/vehicles/4").Result;
if (genericResponse != null)
{
    Console.WriteLine(genericResponse.Name);
}
else
{
    Console.WriteLine("Targeted object does not exist.");
}
System.Console.WriteLine();

SearchResult<Person> skywalkers = service.GetPersonSearchAsync("skywalker").Result;
foreach(Person p in skywalkers.Results)
{
    Console.WriteLine(p.Name);
}
System.Console.WriteLine();

var genericSearch = service.GetSearchAsync<Vehicle>("speeder", "vehicles").Result;
var vehicleSearch = service.GetVehicleSearchAsync("speeder").Result;

foreach (Vehicle v in vehicleSearch.Results)
{
    System.Console.WriteLine(v.Manufacturer);
}


namespace Tests;
using Microsoft.AspNetCore.Mvc.Testing;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup> where TStartup : class {

}
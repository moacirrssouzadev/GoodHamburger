using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GoodHamburger.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => 
{
    var config = sp.GetRequiredService<IConfiguration>();
    var apiBaseUrl = config["ApiBaseUrl"];
    
    if (string.IsNullOrEmpty(apiBaseUrl))
    {
        // Fallback para localhost se não configurado
        apiBaseUrl = "http://localhost:8080/";
    }
    
    return new HttpClient 
    { 
        BaseAddress = new Uri(apiBaseUrl) 
    };
});

await builder.Build().RunAsync();

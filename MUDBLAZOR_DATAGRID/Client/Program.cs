using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MUDBLAZOR_DATAGRID.Client;
using MUDBLAZOR_DATAGRID.Client.Utils.HttpRepository;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}api/") });
builder.Services.AddScoped<IHttpClientRepository, HttpClientRepository>();


builder.Services.AddMudServices();

await builder.Build().RunAsync();

using BlazorComponentsDemo;
using BlazorComponentsDemo.Services.Contracts;
using BlazorComponentsDemo.Services.Implementations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IDataAccessService, DataAccessService>();
builder.Services.AddScoped<ContextMenuService, ContextMenuService>();

await builder.Build().RunAsync();

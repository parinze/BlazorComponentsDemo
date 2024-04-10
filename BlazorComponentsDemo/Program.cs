using BlazorComponentsDemo;
using BlazorComponentsDemo.DataModels.Data;
using BlazorComponentsDemo.DataModels.Models;
using BlazorComponentsDemo.Services;
using BlazorComponentsDemo.Services.Contracts;
using BlazorComponentsDemo.Services.Implementations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IDataAccessService, DataAccessService>();
await builder.Build().RunAsync();

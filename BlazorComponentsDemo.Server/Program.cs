using BlazorComponentsDemo.DataModels.Data;
using BlazorComponentsDemo.Server.Services.Contracts;
using BlazorComponentsDemo.Server.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPeopleTestDataService, PeopleTestDataService>();
builder.Services.AddDbContext<EFTestDataDBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
	.AllowAnyMethod()
	.AllowAnyHeader()
	.SetIsOriginAllowed(_ => true) // allow any origin 
	.AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();

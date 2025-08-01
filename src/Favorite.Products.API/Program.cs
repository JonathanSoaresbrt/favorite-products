using Favorite.Products.API.Ioc;
using Favorite.Products.API.Extensions;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterDependencyInjection(configuration);
builder.Services.ConfigureAuthentication(configuration);
builder.Services.ConfigureVersioning();
builder.Services.ConfigureHttpClients();

var app = builder.Build();

await app.ApplyMigrations();

app.ConfigureMiddlewares();

app.MapControllers();
app.Run();

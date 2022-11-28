using Flurl.Http;
using Flurl.Http.Configuration;
using Framework.Api;
using Framework.Api.Base;
using Framework.Properties.Constants;

var builder = WebApplication.CreateBuilder(args);
#region FlurlDependencies
builder.Services.AddSingleton<IFlurlClientFactory, DefaultFlurlClientFactory>();
#endregion
var app = builder.Build();
ApiServerConfiguration.ROOT_URL = builder.Configuration["Configuration:ROOT_URL"];

app.MapGet("/", () => "Hello World!");

app.Run();

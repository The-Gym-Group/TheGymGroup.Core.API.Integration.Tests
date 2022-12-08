using Flurl.Http;
using Flurl.Http.Configuration;
using Framework.Api;
using Framework.Api.Base;
using Framework.Properties.Constants;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
#region FlurlDependencies
builder.Services.AddSingleton<IFlurlClientFactory, DefaultFlurlClientFactory>();
#endregion
var app = builder.Build();
ApiServerConfiguration.ROOT_URL = builder.Configuration["Configuration:ROOT_URL"];
ApiServerConfiguration.ENVIRONMENT = builder.Configuration["Configuration:ENVIRONMENT"];
ApiServerConfiguration.ConfigurationManager = builder.Configuration;

app.MapGet("/", () => "Hello World!");

app.Run();

public partial class Program { }

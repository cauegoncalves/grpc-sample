using Calculator;
using CalculatorServer;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var hostbuilder = new HostBuilder()
    .ConfigureLogging(logging => 
    {
        logging.AddFilter("Grpc", LogLevel.Debug);
        logging.AddConsole();
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<CalculatorServiceImpl>();
        services.AddLogging(logging => logging.AddConsole());
        services.AddHostedService<AppServer>();
    });

await hostbuilder.RunConsoleAsync();
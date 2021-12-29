using Calculator;
using CalculatorServer;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class AppServer : IHostedService
{
    private readonly Server server = new Server();
    private readonly CalculatorServiceImpl _calculatorService;
    private readonly ILogger<AppServer> _logger;

    public AppServer(CalculatorServiceImpl calculatorService, ILogger<AppServer> logger)
    {
        _calculatorService = calculatorService;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var host = "localhost";
        var port = 50001;
        _logger.LogInformation($"Grpc server starting...");
        server.Services.Add(CalculatorService.BindService(_calculatorService));
        server.Ports.Add(new ServerPort(host, port, ServerCredentials.Insecure));
        server.Start();
        _logger.LogInformation($"Grpc server started at {host}:{port}...");
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Grpc server shutting down...");
        await server.ShutdownAsync();
        _logger.LogInformation("Grpc server shutted down.");
    }
}
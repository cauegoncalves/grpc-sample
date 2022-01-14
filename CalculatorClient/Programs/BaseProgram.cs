using Grpc.Core;
using static Calculator.CalculatorService;

namespace CalculatorClient.Programs
{
    internal abstract class BaseProgram : IProgram
    {
        public abstract Task RunProgram();

        protected async Task<CalculatorServiceConnectionHandler> Connect()
        {
            var channel = new Channel("127.0.0.1:50001", ChannelCredentials.Insecure);
            await channel.ConnectAsync();
            Console.WriteLine("Connected to the server...");
            var client = new CalculatorServiceClient(channel);
            return new CalculatorServiceConnectionHandler(channel, client);
        }

        protected async Task InternalRunProgram(string programTitle, Func<CalculatorServiceClient, Task> programTask)
        {
            Console.Clear();
            PrintProgramTitle(programTitle);
            await using (var handler = await Connect())
            {
                var client = handler.Client;
                await programTask(client);
            }
        }

        private void PrintProgramTitle(string programTitle)
        {
            var line = new string('*', 60);
            Console.WriteLine(line);
            Console.WriteLine(programTitle);
            Console.WriteLine(line);
        }

        protected string? Read(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }

    internal class CalculatorServiceConnectionHandler : IAsyncDisposable
    {
        private readonly Channel _channel;
        private readonly CalculatorServiceClient _client;

        public CalculatorServiceClient Client => _client;

        public CalculatorServiceConnectionHandler(Channel channel, CalculatorServiceClient client)
        {
            _channel = channel;
            _client = client;
        }

        public async ValueTask DisposeAsync()
        {
            await _channel.ShutdownAsync();
        }
    }
}

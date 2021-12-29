using Calculator;
using Grpc.Core;
using static Calculator.CalculatorService;

namespace CalculatorClient.Programs
{
    internal class MaxProgram : BaseProgram
    {
        public override async Task RunProgram() => await InternalRunProgram("Max", RunMaxProgram);

        private async Task RunMaxProgram(CalculatorServiceClient client)
        {
            var stream = client.Max();
            Task.Run(async () =>
            {
                while (await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine($"Max is {stream.ResponseStream.Current.Max} now");
                }
            });

            bool validNumber;
            do
            {
                await Task.Delay(10);
                var value = Read("Value: ");
                validNumber = int.TryParse(value, out var parsedValue);
                if (validNumber)
                {
                    await stream.RequestStream.WriteAsync(new MaximumRequest { Value = parsedValue });
                }
            } while (validNumber);
            await stream.RequestStream.CompleteAsync();
        }
    }
}

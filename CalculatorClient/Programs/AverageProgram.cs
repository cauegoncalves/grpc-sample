using Calculator;
using static Calculator.CalculatorService;

namespace CalculatorClient.Programs
{
    internal class AverageProgram : BaseProgram
    {
        public override async Task RunProgram() => await InternalRunProgram("Average", RunAverageProgram);

        private async Task RunAverageProgram(CalculatorServiceClient client)
        {
            var stream = client.Average();

            bool validNumber;
            do
            {
                string? value = Read("Value: ");
                validNumber = int.TryParse(value, out var parsedValue);
                if (validNumber)
                {
                    await stream.RequestStream.WriteAsync(new AverageRequest { Value = parsedValue });
                }
            } while (validNumber);
            await stream.RequestStream.CompleteAsync();

            var response = await stream.ResponseAsync;
            Console.WriteLine(response.Average);
        }
    }
}

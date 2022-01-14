using Calculator;
using static Calculator.CalculatorService;

namespace CalculatorClient.Programs
{
    internal class SumProgram : BaseProgram
    {
        public override async Task RunProgram() => await InternalRunProgram("Sum", RunSumProgram);

        private async Task RunSumProgram(CalculatorServiceClient client)
        {
            var a = Read("A: ");
            var b = Read("B: ");

            var request = new CalculatorRequest
            {
                A = Convert.ToInt32(a),
                B = Convert.ToInt32(b),
            };

            var result = await client.SumAsync(request);
            Console.WriteLine($"The sum is {result.Result}.");
        }
    }
}

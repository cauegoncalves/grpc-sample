using Calculator;
using Grpc.Core;
using static Calculator.CalculatorService;

namespace CalculatorClient.Programs
{
    internal class DecomposeProgram : BaseProgram
    {
        public override async Task RunProgram() => await InternalRunProgram("Decompose", RunDecomposeProgram);

        private async Task RunDecomposeProgram(CalculatorServiceClient client)
        {
            var value = Read("Value: ");

            var request = new NumberDecompositionRequest
            {
                Value = Convert.ToInt32(value)
            };

            var result = client.DecomposeNumber(request);

            while (await result.ResponseStream.MoveNext())
            {
                Console.WriteLine(result.ResponseStream.Current.Factor);
            }
        }
    }
}

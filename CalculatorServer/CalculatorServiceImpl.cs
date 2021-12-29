using Calculator;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using static Calculator.CalculatorService;

namespace CalculatorServer
{
    public class CalculatorServiceImpl : CalculatorServiceBase
    {
        private readonly ILogger<CalculatorServiceImpl> _logger;

        public CalculatorServiceImpl(ILogger<CalculatorServiceImpl> logger)
        {
            _logger = logger;
        }

        public override Task<CalculatorResponse> Sum(CalculatorRequest request, ServerCallContext context)
        {
            var result = request.A + request.B;
            var response = new CalculatorResponse() { Result = result };
            return Task.FromResult(response);
        }

        public override async Task DecompositeNumber(NumberDecompositionRequest request, IServerStreamWriter<NumberDecompositionResponse> responseStream, ServerCallContext context)
        {
            var value = request.Value;
            var factor = 2;
            while (value > 1)
            {
                if (value % factor == 0)
                {
                    var response = new NumberDecompositionResponse { Factor = factor };
                    await responseStream.WriteAsync(response);
                    await Task.Delay(500);
                    
                    value /= factor;
                }
                else
                    factor++;
            }
        }

        public override async Task<AverageResponse> Average(IAsyncStreamReader<AverageRequest> requestStream, ServerCallContext context)
        {
            var count = 0;
            var sum = 0;

            while (await requestStream.MoveNext())
            {
                sum += requestStream.Current.Value;
                count++;
            }

            var response = new AverageResponse()
            {
                Average = sum / count
            };

            return response;
        }

        public override async Task Max(IAsyncStreamReader<MaximumRequest> requestStream, IServerStreamWriter<MaximumResponse> responseStream, ServerCallContext context)
        {
            var max = 0;
            while (await requestStream.MoveNext())
            {
                if (requestStream.Current.Value > max)
                {
                    max = requestStream.Current.Value;
                    await responseStream.WriteAsync(new MaximumResponse { Max = max });
                }
            }
        }
    }
}

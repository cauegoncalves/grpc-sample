using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClient.Programs
{
    internal class ExitProgram : IProgram
    {
        public Task RunProgram()
        {
            Environment.Exit(0);
            return Task.CompletedTask;
        }
    }
}

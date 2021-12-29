using CalculatorClient.Programs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClient
{
    internal static class ProgramFactory
    {

        private static readonly Dictionary<EnumProgramType, IProgram> _programs = new()
        {
            { EnumProgramType.AVERAGE, new AverageProgram() },
            { EnumProgramType.MAX, new MaxProgram() },
            { EnumProgramType.DECOMPOSE, new DecomposeProgram() },
            { EnumProgramType.SUM, new SumProgram() },
            { EnumProgramType.EXIT, new ExitProgram() },
        };

        public static IProgram Create(EnumProgramType program)
        {
            return _programs[program];
        }

    }
}

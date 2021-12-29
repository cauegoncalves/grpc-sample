using CalculatorClient;

do {
    var menu = PrintOptions();
    
    var validOption = Enum.TryParse<EnumProgramType>(menu, out var menuSelection) && Enum.IsDefined<EnumProgramType>(menuSelection);

    if (!validOption)
    {
        Console.Write("Invalid option. Try again.");
        Console.ReadKey();
        continue;
    }

    var program = ProgramFactory.Create(menuSelection);
    await program.RunProgram();
    
    Console.ReadKey();
} while (true);

string? PrintOptions()
{
    Console.Clear();
    var names = Enum.GetNames(typeof(EnumProgramType));
    var values = Enum.GetValues(typeof(EnumProgramType));
    Console.WriteLine("Select the program:");
    for (int i = 0; i < names.Length; i++)
    {
        Console.WriteLine($"{Convert.ToInt32(values.GetValue(i))} - {names[i]}");
    }
    return Console.ReadLine();
}
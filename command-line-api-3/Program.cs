using System;
using System.CommandLine;
using System.CommandLine.Parsing;

public class Program
{
    public string String { get; set; }
    public bool Bool { get; set; }

    static void Run(Program options)
    {
        Console.WriteLine($"Bool option: {options.Bool}");
        Console.WriteLine($"String option: {options.String}");
    }

    private static int Main(string[] args)
    {
        var boolOption = new Option<bool>(new[] { "--bool", "-b" }, "Bool option");
        var stringOption = new Option<string>(new[] { "--string", "-s" }, "String option");

        var command = new RootCommand
        {
            boolOption,
            stringOption
        };

        var parser = new Parser(command);

        var parseResult = parser.Parse(args);

        var model = new Program
        {
            Bool = parseResult.ValueForOption<bool>(boolOption.Name),
            String = parseResult.ValueForOption<string>(stringOption.Name),
        };

        Run(model);

        return 1;
    }
}
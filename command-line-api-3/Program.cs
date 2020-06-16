using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

public class Program
{
    public string String { get; set; }
    public bool Bool { get; set; }

    static void Run(Program options)
    {
        Console.WriteLine($"Bool option: {options.Bool}");
        Console.WriteLine($"String option: {options.String}");
    }

    private static async Task<int> Main(string[] args)
    {
        var boolOption = new Option<bool>(new[] { "--bool", "-b" }, "Bool option");
        var stringOption = new Option<string>(new[] { "--string", "-s" }, "String option");

        var command = new RootCommand
        {
            boolOption,
            stringOption
        };

        command.Handler = CommandHandler.Create<ParseResult>(parseResult =>
        {
            var model = new Program
            {
                Bool = parseResult.ValueForOption<bool>(boolOption.Name),
                String = parseResult.ValueForOption<string>(stringOption.Name),
            };
            Run(model);
        });

        var parser = new Parser(command);

        return await parser.InvokeAsync(args);
    }
}
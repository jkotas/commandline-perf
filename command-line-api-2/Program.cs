using System;
using System.CommandLine;
using System.CommandLine.Builder;
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
        RootCommand command = new RootCommand();
        command.AddOption(new Option<bool>(new[] { "--bool", "-b" }, "Bool option"));
        command.AddOption(new Option<string>(new[] { "--string", "-s" }, "String option"));

        command.Handler = CommandHandler.Create<Program>(p => Run(p));

        var parser = new Parser(command);

        return await parser.InvokeAsync(args);
    }
}

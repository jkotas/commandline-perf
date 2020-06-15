using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

class Program
{
    public string StringOption { get; set; }
    public bool BoolOption { get; set; }

    static void Run(Program options)
    {
        Console.WriteLine($"Bool option: {options.BoolOption}");
        Console.WriteLine($"String option: {options.StringOption}");
    }

    private static async Task<int> Main(string[] args)
    {
        RootCommand command = new RootCommand();
        command.AddOption(new Option<bool>(new[] { "--bool", "-b" }, "Bool option"));
        command.AddOption(new Option<string>(new[] { "--string", "-s" }, "String option"));

        command.Handler = CommandHandler.Create<Program>(Run);

        return await command.InvokeAsync(args);
    }
}

using System;
using System.CommandLine;

public class Program
{
    static void Run(bool boolean, string text)
    {
        Console.WriteLine($"Bool option: {text}");
        Console.WriteLine($"String option: {boolean}");
    }

    private static int Main(string[] args)
    {
        Option<bool> boolOption = new Option<bool>("--bool", "-b") { Description = "Bool option" };
        Option<string> stringOption = new Option<string>("--string", "-s") { Description = "String option" };

        RootCommand command = new ()
        {
            boolOption,
            stringOption,
        };

        command.SetAction(ctx => Run(ctx.GetValue(boolOption), ctx.GetValue(stringOption)));

        return new CommandLineConfiguration(command).Invoke(args);
    }
}
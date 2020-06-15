using System;
using System.CommandLine;

class Program
{
    string _stringOption = "";
    bool _boolOption;

    int Run(string[] args)
    {
        ArgumentSyntax argSyntax = ArgumentSyntax.Parse(args, syntax =>
        {
            syntax.DefineOption("b|bool", ref _boolOption, "Bool option");
            syntax.DefineOption("s|string", ref _stringOption, "String option");
        });

        Console.WriteLine($"Bool option: {_boolOption}");
        Console.WriteLine($"String option: {_stringOption}");

        return 0;
    }

    static int Main(string[] args)
    {
        return new Program().Run(args);
    }
}

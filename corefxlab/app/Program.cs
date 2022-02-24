using System;
using System.CommandLine;
using System.Threading.Tasks;

class Program
{
    string _stringOption = "";
    bool _boolOption;

    Task<int> Run(string[] args)
    {
        ArgumentSyntax argSyntax = ArgumentSyntax.Parse(args, syntax =>
        {
            syntax.DefineOption("b|bool", ref _boolOption, "Bool option");
            syntax.DefineOption("s|string", ref _stringOption, "String option");
        });

        Console.WriteLine($"Bool option: {_boolOption}");
        Console.WriteLine($"String option: {_stringOption}");

        return Task.FromResult<int>(0);
    }

    static Task<int> Main(string[] args)
    {
        return new Program().Run(args);
    }
}

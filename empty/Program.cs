using System;
using System.Threading.Tasks;

namespace empty
{
    class Program
    {
        static Task<int> Main(string[] args)
        {
            string stringOption = "";
            bool boolOption = false;
            
            Console.WriteLine($"Bool option: {stringOption}");
            Console.WriteLine($"String option: {boolOption}");
            
            return Task.FromResult<int>(0);
        }
    }
}

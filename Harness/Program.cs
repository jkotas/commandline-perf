using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Microsoft.DotNet.PlatformAbstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Harness
{
    internal class Program
    {
        static void Main(string[] args) => BenchmarkRunner.Run<Perf_Startup>(
            DefaultConfig.Instance
                .AddJob(Job.Default
                    .WithWarmupCount(1)
                    .RunOncePerIteration()
                    .WithIterationCount(20)),
            args);
    }

    public class Perf_Startup
    {
        private Lazy<string> RootFolderPath = new Lazy<string>(GetRootFolderPath);

        [Params("", "--bool -s test")]
        public string Args { get; set; }

        public string ExeExtension => OperatingSystem.IsWindows() ? ".exe" : "";

        [Benchmark]
        public int Empty()
            => RunProcess(Path.Combine(RootFolderPath.Value, "empty", "bin", "Release", "net6.0", $"empty{ExeExtension}"));

        [Benchmark]
        public int EmptyAOT()
            => RunProcess(Path.Combine(RootFolderPath.Value, "empty-aot", "bin", "Release", "net6.0",
                GetPortableRuntimeIdentifier(), "publish", $"empty-aot{ExeExtension}"));

        [Benchmark]
        public int Corefxlab()
            => RunProcess(Path.Combine(RootFolderPath.Value, "corefxlab", "app", "bin", "Release", "net6.0", $"corefxlab{ExeExtension}"));

        [Benchmark]
        public int SystemCommandLineBefore()
            => RunProcess(Path.Combine(RootFolderPath.Value, "command-line-api", "before", "bin", "Release", "net6.0", $"command-line-api-before{ExeExtension}"));

        [Benchmark]
        public int SystemCommandLineAfter()
            => RunProcess(Path.Combine(RootFolderPath.Value, "command-line-api", "after", "bin", "Release", "net6.0", $"command-line-api-after{ExeExtension}"));

        [Benchmark]
        public int SystemCommandLineAfterR2R()
            => RunProcess(Path.Combine(RootFolderPath.Value, "command-line-api", "after_r2r", "bin", "Release", "net6.0",
                    GetPortableRuntimeIdentifier(), "publish", $"command-line-api-after-r2r{ExeExtension}"));

        [Benchmark]
        public int SystemCommandLineAfterAOT()
            => RunProcess(Path.Combine(RootFolderPath.Value, "command-line-api", "after_aot", "bin", "Release", "net6.0",
                    GetPortableRuntimeIdentifier(), "publish", $"command-line-api-after-aot{ExeExtension}"));

        private static string GetRootFolderPath()
        {
            DirectoryInfo directoryInfo = new(Directory.GetCurrentDirectory());
            while (directoryInfo != null)
            {
                if (directoryInfo.EnumerateFiles("CommandLinePerf.sln").Any())
                {
                    return directoryInfo.FullName;
                }

                directoryInfo = directoryInfo.Parent;
            }

            throw new Exception("Unable to find root directory path!");
        }

        private static string GetPortableRuntimeIdentifier()
        {
            string osPart = OperatingSystem.IsWindows() ? "win" : (OperatingSystem.IsMacOS() ? "osx" : "linux");
            return $"{osPart}-{RuntimeEnvironment.RuntimeArchitecture}";
        }

        private int RunProcess(string executablePath)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(executablePath, Args);

            using Process process = Process.Start(processStartInfo);
            process.WaitForExit();
            return process.ExitCode;
        }
    }
}

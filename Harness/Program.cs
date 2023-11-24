using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Microsoft.DotNet.PlatformAbstractions;
using Perfolizer.Horology;
using Perfolizer.Mathematics.OutlierDetection;
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
                    .WithWarmupCount(3)
                    .WithEvaluateOverhead(false)
                    .WithOutlierMode(OutlierMode.DontRemove)
                    .RunOncePerIteration()
                    .WithIterationCount(100)),
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
            => RunProcess(Path.Combine(RootFolderPath.Value, "empty", "bin", "Release", "net7.0", GetPortableRuntimeIdentifier(), "publish", $"empty{ExeExtension}"));

        [Benchmark]
        public int EmptyAOT()
            => RunProcess(Path.Combine(RootFolderPath.Value, "empty-aot", "bin", "Release", "net7.0",
                GetPortableRuntimeIdentifier(), "native", $"empty-aot{ExeExtension}"));

        [Benchmark]
        public int Corefxlab()
            => RunProcess(Path.Combine(RootFolderPath.Value, "corefxlab", "app", "bin", "Release", "net7.0", GetPortableRuntimeIdentifier(), "publish", $"corefxlab{ExeExtension}"));

        [Benchmark]
        public int SystemCommandLine2021()
            => RunProcess(Path.Combine(RootFolderPath.Value, "command-line-api", "2021", "bin", "Release", "net7.0", GetPortableRuntimeIdentifier(), "publish", $"command-line-api-2021{ExeExtension}"));

        [Benchmark]
        public int SystemCommandLine2022()
            => RunProcess(Path.Combine(RootFolderPath.Value, "command-line-api", "2022", "bin", "Release", "net7.0", GetPortableRuntimeIdentifier(), "publish", $"command-line-api-2022{ExeExtension}"));

        [Benchmark(Baseline = true)]
        public int SystemCommandLineNow()
            => RunProcess(Path.Combine(RootFolderPath.Value, "command-line-api", "now", "bin", "Release", "net7.0", GetPortableRuntimeIdentifier(), "publish", $"command-line-api-now{ExeExtension}"));

        [Benchmark]
        public int SystemCommandLineNowR2R()
            => RunProcess(Path.Combine(RootFolderPath.Value, "command-line-api", "now_r2r", "bin", "Release", "net7.0",
                    GetPortableRuntimeIdentifier(), "publish", $"command-line-api-now-r2r{ExeExtension}"));

        [Benchmark]
        public int SystemCommandLineNowAOT()
            => RunProcess(Path.Combine(RootFolderPath.Value, "command-line-api", "now_aot", "bin", "Release", "net7.0",
                    GetPortableRuntimeIdentifier(), "native", $"command-line-api-now-aot{ExeExtension}"));

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

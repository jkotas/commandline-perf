# How to run the benchmarks

From the repo root:

```cmd
dotnet publish -c Release -r win-x64
dotnet run -c Release --project .\Harness\Harness.csproj
```

# Sample results

```ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
AMD Ryzen Threadripper PRO 3945WX 12-Cores, 1 CPU, 24 logical and 12 physical cores
```


|                    Method |           Args |     Mean |    Error |   StdDev |
|-------------------------- |--------------- |---------:|---------:|---------:|
|                     Empty |                | 35.80 ms | 1.173 ms | 1.304 ms |
|                  EmptyAOT |                | 21.30 ms | 0.747 ms | 0.860 ms |
|                 Corefxlab |                | 48.07 ms | 1.067 ms | 1.229 ms |
|   SystemCommandLineBefore |                | 75.51 ms | 1.197 ms | 1.330 ms |
|    SystemCommandLineAfter |                | 54.31 ms | 1.261 ms | 1.452 ms |
| SystemCommandLineAfterR2R |                | 70.40 ms | 1.382 ms | 1.592 ms |
| SystemCommandLineAfterAOT |                | 27.40 ms | 0.870 ms | 1.002 ms |
|                     Empty | --bool -s test | 35.67 ms | 1.130 ms | 1.301 ms |
|                  EmptyAOT | --bool -s test | 21.24 ms | 0.643 ms | 0.714 ms |
|                 Corefxlab | --bool -s test | 54.72 ms | 1.214 ms | 1.398 ms |
|   SystemCommandLineBefore | --bool -s test | 79.65 ms | 1.108 ms | 1.232 ms |
|    SystemCommandLineAfter | --bool -s test | 60.50 ms | 1.123 ms | 1.248 ms |
| SystemCommandLineAfterR2R | --bool -s test | 73.21 ms | 1.332 ms | 1.480 ms |
| SystemCommandLineAfterAOT | --bool -s test | 28.09 ms | 0.651 ms | 0.697 ms |
## ThenBy

### Source
[ThenBy.cs](../../QuickLinq.Benchmarks/Cases/ThenBy.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|            Method |     Mean |     Error |    StdDev | Ratio |    Gen0 |    Gen1 |    Gen2 | Allocated | Alloc Ratio |
|------------------ |---------:|----------:|----------:|------:|--------:|--------:|--------:|----------:|------------:|
|              Linq | 1.662 ms | 0.0011 ms | 0.0010 ms |  1.00 | 97.6563 | 48.8281 | 48.8281 |  280490 B |       1.000 |
| QuickLinqDelegate | 1.702 ms | 0.0016 ms | 0.0014 ms |  1.02 |       - |       - |       - |      65 B |       0.000 |
|   QuickLinqStruct | 1.225 ms | 0.0115 ms | 0.0102 ms |  0.74 |       - |       - |       - |       1 B |       0.000 |

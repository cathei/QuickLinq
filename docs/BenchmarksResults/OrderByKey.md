## OrderByKey

### Source
[OrderByKey.cs](../../QuickLinq.Benchmarks/Cases/OrderByKey.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |       Mean |    Error |   StdDev | Ratio |    Gen0 | Allocated | Alloc Ratio |
|------------------- |-----------:|---------:|---------:|------:|--------:|----------:|------------:|
|        LinqWithKey | 1,170.1 μs |  1.30 μs |  1.01 μs |  1.00 | 54.6875 |  120313 B |       1.000 |
|  QuickLinqDelegate | 1,361.0 μs |  1.20 μs |  1.06 μs |  1.16 |       - |       1 B |       0.000 |
|    QuickLinqStruct | 1,145.0 μs | 10.93 μs | 10.22 μs |  0.98 |       - |       1 B |       0.000 |
| StructLinqDelegate | 1,228.3 μs |  4.85 μs |  4.05 μs |  1.05 |       - |     362 B |       0.003 |
|   StructLinqStruct |   908.9 μs |  8.28 μs |  7.74 μs |  0.77 |       - |     129 B |       0.001 |

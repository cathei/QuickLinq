## OrderBy

### Source
[OrderBy.cs](../../QuickLinq.Benchmarks/Cases/OrderBy.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |       Mean |    Error |  StdDev | Ratio |    Gen0 | Allocated | Alloc Ratio |
|------------------- |-----------:|---------:|--------:|------:|--------:|----------:|------------:|
|               Linq | 1,167.0 μs |  3.12 μs | 2.61 μs |  1.00 | 54.6875 |  120313 B |       1.000 |
|  QuickLinqDelegate | 1,309.9 μs |  7.51 μs | 7.02 μs |  1.12 |       - |       1 B |       0.000 |
|    QuickLinqStruct | 1,078.9 μs |  4.55 μs | 4.03 μs |  0.92 |       - |       1 B |       0.000 |
| StructLinqDelegate | 1,220.6 μs |  4.68 μs | 4.38 μs |  1.05 |       - |     346 B |       0.003 |
|   StructLinqStruct |   907.0 μs | 10.28 μs | 9.62 μs |  0.78 |       - |     145 B |       0.001 |

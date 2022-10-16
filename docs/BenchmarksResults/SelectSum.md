## SelectSum

### Source
[SelectSum.cs](../../QuickLinq.Benchmarks/Cases/SelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |      Mean |     Error |    StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|------:|-------:|----------:|------------:|
|            ForLoop |  9.458 μs | 0.0332 μs | 0.0277 μs |  0.24 |      - |         - |        0.00 |
|        ForEachLoop | 25.831 μs | 0.1788 μs | 0.1585 μs |  0.66 |      - |      40 B |        0.45 |
|               Linq | 38.936 μs | 0.0376 μs | 0.0314 μs |  1.00 |      - |      88 B |        1.00 |
|  QuickLinqDelegate | 26.372 μs | 0.1208 μs | 0.1130 μs |  0.68 |      - |         - |        0.00 |
|    QuickLinqStruct | 11.757 μs | 0.0402 μs | 0.0376 μs |  0.30 |      - |         - |        0.00 |
| StructLinqDelegate | 14.312 μs | 0.1058 μs | 0.0990 μs |  0.37 | 0.0153 |      56 B |        0.64 |
|   StructLinqStruct |  9.403 μs | 0.0050 μs | 0.0047 μs |  0.24 |      - |         - |        0.00 |
|  HyperLinqDelegate | 34.653 μs | 0.0744 μs | 0.0660 μs |  0.89 |      - |         - |        0.00 |
|    HyperLinqStruct | 34.722 μs | 0.0911 μs | 0.0852 μs |  0.89 |      - |         - |        0.00 |

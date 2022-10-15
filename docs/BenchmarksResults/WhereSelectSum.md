## WhereSelectSum

### Source
[WhereSelectSum.cs](../../QuickLinq.Benchmarks/Cases/WhereSelectSum.cs)

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
|            ForLoop |  7.874 μs | 0.0094 μs | 0.0074 μs |  0.13 |      - |         - |        0.00 |
|               Linq | 60.387 μs | 0.1827 μs | 0.1619 μs |  1.00 | 0.0610 |     160 B |        1.00 |
|  QuickLinqDelegate | 22.101 μs | 0.1289 μs | 0.1205 μs |  0.37 |      - |         - |        0.00 |
|    QuickLinqStruct |  8.281 μs | 0.0387 μs | 0.0362 μs |  0.14 |      - |         - |        0.00 |
| StructLinqDelegate | 22.119 μs | 0.0243 μs | 0.0215 μs |  0.37 | 0.0305 |      88 B |        0.55 |
|   StructLinqStruct | 17.423 μs | 0.0924 μs | 0.0864 μs |  0.29 |      - |         - |        0.00 |
|  HyperLinqDelegate | 22.053 μs | 0.0121 μs | 0.0114 μs |  0.37 |      - |         - |        0.00 |
|    HyperLinqStruct | 17.333 μs | 0.0193 μs | 0.0171 μs |  0.29 |      - |         - |        0.00 |

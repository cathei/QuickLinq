## WhereSelectSum

### Source
[WhereSelectSum.cs](../../QuickLinq.Benchmarks/Cases/WhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.2130/21H1/May2021Update)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT AVX2


```
|             Method |      Mean |     Error |    StdDev | Ratio | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|------:|----------:|------------:|
|            ForLoop |  4.095 μs | 0.0797 μs | 0.0979 μs |  0.05 |         - |        0.00 |
|        ForEachLoop | 41.434 μs | 0.1697 μs | 0.1417 μs |  0.49 |      40 B |        0.25 |
|               Linq | 85.190 μs | 0.4151 μs | 0.3680 μs |  1.00 |     160 B |        1.00 |
|  QuickLinqDelegate | 25.312 μs | 0.1115 μs | 0.0931 μs |  0.30 |         - |        0.00 |
|    QuickLinqStruct |  7.834 μs | 0.1537 μs | 0.1578 μs |  0.09 |         - |        0.00 |
| StructLinqDelegate | 25.632 μs | 0.4944 μs | 0.4624 μs |  0.30 |      88 B |        0.55 |
|   StructLinqStruct | 13.733 μs | 0.0667 μs | 0.0624 μs |  0.16 |         - |        0.00 |
|  HyperLinqDelegate | 26.974 μs | 0.5030 μs | 0.4200 μs |  0.32 |         - |        0.00 |
|    HyperLinqStruct | 13.826 μs | 0.0254 μs | 0.0238 μs |  0.16 |         - |        0.00 |

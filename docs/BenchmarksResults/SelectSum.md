## SelectSum

### Source
[SelectSum.cs](../../QuickLinq.Benchmarks/Cases/SelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.2130/21H1/May2021Update)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT AVX2


```
|             Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|----------:|------:|--------:|----------:|------------:|
|            ForLoop |  7.597 μs | 0.0467 μs | 0.0437 μs |  7.573 μs |  0.15 |    0.00 |         - |        0.00 |
|        ForEachLoop | 39.309 μs | 0.6655 μs | 0.6225 μs | 39.233 μs |  0.75 |    0.02 |      40 B |        0.45 |
|               Linq | 52.246 μs | 0.8747 μs | 0.7304 μs | 51.933 μs |  1.00 |    0.00 |      88 B |        1.00 |
|  QuickLinqDelegate | 18.130 μs | 0.3350 μs | 0.6125 μs | 17.819 μs |  0.35 |    0.01 |         - |        0.00 |
|    QuickLinqStruct |  9.916 μs | 0.7830 μs | 2.2963 μs |  9.006 μs |  0.18 |    0.04 |         - |        0.00 |
| StructLinqDelegate | 15.154 μs | 0.0589 μs | 0.0492 μs | 15.139 μs |  0.29 |    0.00 |      56 B |        0.64 |
|   StructLinqStruct |  7.543 μs | 0.0356 μs | 0.0333 μs |  7.530 μs |  0.14 |    0.00 |         - |        0.00 |
|  HyperLinqDelegate | 28.027 μs | 0.3662 μs | 0.3426 μs | 27.931 μs |  0.54 |    0.01 |         - |        0.00 |
|    HyperLinqStruct | 27.389 μs | 0.0988 μs | 0.0924 μs | 27.400 μs |  0.52 |    0.01 |         - |        0.00 |

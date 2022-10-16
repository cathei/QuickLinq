## SelectCount

### Source
[SelectCount.cs](../../QuickLinq.Benchmarks/Cases/SelectCount.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |          Mean |      Error |     StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|------------------- |--------------:|-----------:|-----------:|------:|-------:|----------:|------------:|
|               Linq | 14,119.864 ns | 57.2928 ns | 47.8421 ns | 1.000 | 0.0305 |      88 B |        1.00 |
|  QuickLinqDelegate |      5.800 ns |  0.0359 ns |  0.0335 ns | 0.000 |      - |         - |        0.00 |
|    QuickLinqStruct |      5.448 ns |  0.0242 ns |  0.0226 ns | 0.000 |      - |         - |        0.00 |
| StructLinqDelegate |     13.803 ns |  0.0334 ns |  0.0313 ns | 0.001 | 0.0268 |      56 B |        0.64 |
|   StructLinqStruct |      3.452 ns |  0.0093 ns |  0.0087 ns | 0.000 |      - |         - |        0.00 |
|  HyperLinqDelegate |      5.542 ns |  0.0208 ns |  0.0195 ns | 0.000 |      - |         - |        0.00 |
|    HyperLinqStruct |      3.312 ns |  0.0849 ns |  0.0834 ns | 0.000 |      - |         - |        0.00 |

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
|             Method |         list |       Mean |   Error |  StdDev |    Gen0 | Allocated |
|------------------- |------------- |-----------:|--------:|--------:|--------:|----------:|
|               Linq | Int32[10000] | 1,167.4 μs | 2.21 μs | 1.96 μs | 54.6875 |  120313 B |
|  QuickLinqDelegate | Int32[10000] | 1,275.4 μs | 7.06 μs | 5.89 μs |       - |       1 B |
|    QuickLinqStruct | Int32[10000] |   992.8 μs | 6.42 μs | 5.69 μs |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,203.0 μs | 1.38 μs | 1.29 μs |       - |     346 B |
|   StructLinqStruct | Int32[10000] |   897.1 μs | 1.75 μs | 1.55 μs |       - |     145 B |

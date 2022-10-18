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
|               Linq | Int32[10000] | 1,180.3 μs | 7.99 μs | 7.48 μs | 54.6875 |  120313 B |
|  QuickLinqDelegate | Int32[10000] | 1,193.3 μs | 1.75 μs | 1.63 μs |       - |       1 B |
|    QuickLinqStruct | Int32[10000] |   912.4 μs | 2.73 μs | 2.55 μs |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,203.5 μs | 2.48 μs | 2.19 μs |       - |     346 B |
|   StructLinqStruct | Int32[10000] |   894.9 μs | 1.24 μs | 1.03 μs |       - |     145 B |

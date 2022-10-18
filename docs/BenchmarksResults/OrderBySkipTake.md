## OrderBySkipTake

### Source
[OrderBySkipTake.cs](../../QuickLinq.Benchmarks/Cases/OrderBySkipTake.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |         list |           Mean |       Error |      StdDev |    Gen0 | Allocated |
|------------------- |------------- |---------------:|------------:|------------:|--------:|----------:|
|               **Linq** | **Int32[10000]** |    **93,406.2 ns** |   **262.08 ns** |   **232.33 ns** | **56.5186** |  **120312 B** |
|  QuickLinqDelegate | Int32[10000] |    46,349.9 ns |    86.66 ns |    81.06 ns |       - |         - |
|    QuickLinqStruct | Int32[10000] |    46,269.4 ns |    76.62 ns |    71.67 ns |       - |         - |
| StructLinqDelegate | Int32[10000] | 1,193,493.0 ns | 8,600.86 ns | 7,182.11 ns |       - |     412 B |
|   StructLinqStruct | Int32[10000] |   890,699.7 ns | 7,085.87 ns | 5,917.02 ns |       - |     137 B |
|               **Linq** |    **Int32[20]** |       **543.4 ns** |     **1.43 ns** |     **1.27 ns** |  **0.2632** |     **552 B** |
|  QuickLinqDelegate |    Int32[20] |       434.3 ns |     2.28 ns |     2.13 ns |       - |         - |
|    QuickLinqStruct |    Int32[20] |       345.6 ns |     0.90 ns |     0.75 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       741.1 ns |     2.57 ns |     2.27 ns |  0.0725 |     152 B |
|   StructLinqStruct |    Int32[20] |       461.3 ns |     2.62 ns |     2.46 ns |       - |         - |
|               **Linq** |   **Int32[500]** |     **9,142.9 ns** |    **47.65 ns** |    **44.57 ns** |  **3.0060** |    **6312 B** |
|  QuickLinqDelegate |   Int32[500] |     2,712.5 ns |    10.15 ns |     7.92 ns |       - |         - |
|    QuickLinqStruct |   Int32[500] |     2,601.6 ns |     8.56 ns |     7.15 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    31,511.6 ns |   566.61 ns |   530.01 ns |  0.0610 |     152 B |
|   StructLinqStruct |   Int32[500] |    12,887.9 ns |   255.01 ns |   340.43 ns |       - |         - |

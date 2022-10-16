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
|               **Linq** | **Int32[10000]** |    **93,936.3 ns** |   **359.32 ns** |   **336.11 ns** | **56.5186** |  **120312 B** |
|  QuickLinqDelegate | Int32[10000] |   128,400.2 ns |   490.57 ns |   458.88 ns |       - |         - |
|    QuickLinqStruct | Int32[10000] |    91,712.8 ns | 1,539.17 ns | 1,364.44 ns |       - |         - |
| StructLinqDelegate | Int32[10000] | 1,214,929.2 ns | 4,462.28 ns | 4,174.02 ns |       - |     427 B |
|   StructLinqStruct | Int32[10000] |   888,200.5 ns | 2,902.72 ns | 2,573.18 ns |       - |     133 B |
|               **Linq** |    **Int32[20]** |       **545.0 ns** |     **3.71 ns** |     **3.29 ns** |  **0.2632** |     **552 B** |
|  QuickLinqDelegate |    Int32[20] |       836.5 ns |     2.99 ns |     2.79 ns |       - |         - |
|    QuickLinqStruct |    Int32[20] |       719.3 ns |     4.58 ns |     4.28 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       723.3 ns |     0.74 ns |     0.69 ns |  0.0725 |     152 B |
|   StructLinqStruct |    Int32[20] |       460.6 ns |     2.52 ns |     2.36 ns |       - |         - |
|               **Linq** |   **Int32[500]** |     **9,068.5 ns** |    **32.16 ns** |    **30.08 ns** |  **3.0060** |    **6312 B** |
|  QuickLinqDelegate |   Int32[500] |     9,798.4 ns |    21.41 ns |    16.71 ns |       - |         - |
|    QuickLinqStruct |   Int32[500] |     6,931.0 ns |    11.16 ns |    10.44 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    28,519.1 ns |   212.81 ns |   199.06 ns |  0.0610 |     152 B |
|   StructLinqStruct |   Int32[500] |    12,081.1 ns |    42.43 ns |    37.61 ns |       - |         - |

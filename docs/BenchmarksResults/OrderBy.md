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
|             Method |         list |           Mean |        Error |       StdDev |    Gen0 | Allocated |
|------------------- |------------- |---------------:|-------------:|-------------:|--------:|----------:|
|               **Linq** | **Int32[10000]** | **1,198,395.9 ns** |  **8,296.32 ns** |  **7,760.38 ns** | **54.6875** |  **120313 B** |
|  QuickLinqDelegate | Int32[10000] | 1,381,427.7 ns |  4,915.11 ns |  4,597.60 ns |       - |       1 B |
|    QuickLinqStruct | Int32[10000] | 1,114,989.5 ns |  6,372.83 ns |  5,961.15 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,218,717.5 ns | 11,611.14 ns | 10,861.06 ns |       - |     346 B |
|   StructLinqStruct | Int32[10000] |   900,329.8 ns |  1,689.58 ns |  1,319.11 ns |       - |     129 B |
|               **Linq** |    **Int32[20]** |       **672.3 ns** |      **4.66 ns** |      **4.36 ns** |  **0.2632** |     **552 B** |
|  QuickLinqDelegate |    Int32[20] |       829.9 ns |      2.59 ns |      2.42 ns |       - |         - |
|    QuickLinqStruct |    Int32[20] |       670.9 ns |      1.62 ns |      1.52 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       706.1 ns |      1.59 ns |      1.49 ns |  0.0420 |      88 B |
|   StructLinqStruct |    Int32[20] |       435.2 ns |      1.27 ns |      1.18 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **32,569.9 ns** |    **252.46 ns** |    **210.81 ns** |  **2.9907** |    **6312 B** |
|  QuickLinqDelegate |   Int32[500] |    34,868.3 ns |    553.67 ns |    517.90 ns |       - |         - |
|    QuickLinqStruct |   Int32[500] |    20,134.4 ns |     45.34 ns |     42.41 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    27,942.5 ns |    528.45 ns |    468.46 ns |  0.0305 |      88 B |
|   StructLinqStruct |   Int32[500] |    13,151.5 ns |    261.05 ns |    464.02 ns |       - |         - |

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
|             Method |         list |           Mean |       Error |      StdDev |    Gen0 | Allocated |
|------------------- |------------- |---------------:|------------:|------------:|--------:|----------:|
|               **Linq** | **Int32[10000]** | **1,202,404.7 ns** | **7,907.35 ns** | **7,396.54 ns** | **54.6875** |  **120313 B** |
|  QuickLinqDelegate | Int32[10000] | 1,301,448.4 ns | 7,987.88 ns | 7,471.86 ns |       - |       1 B |
|    QuickLinqStruct | Int32[10000] | 1,092,489.4 ns | 5,776.13 ns | 5,402.99 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,242,431.9 ns | 5,589.50 ns | 4,954.94 ns |       - |     379 B |
|   StructLinqStruct | Int32[10000] |   930,202.9 ns | 8,709.69 ns | 8,147.05 ns |       - |     129 B |
|               **Linq** |    **Int32[20]** |       **689.5 ns** |     **4.09 ns** |     **3.83 ns** |  **0.2632** |     **552 B** |
|  QuickLinqDelegate |    Int32[20] |       739.2 ns |     4.46 ns |     4.17 ns |       - |         - |
|    QuickLinqStruct |    Int32[20] |       651.9 ns |     1.67 ns |     1.48 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       722.2 ns |     3.85 ns |     3.42 ns |  0.0420 |      88 B |
|   StructLinqStruct |    Int32[20] |       445.0 ns |     0.98 ns |     0.87 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **32,779.9 ns** |   **232.06 ns** |   **193.78 ns** |  **2.9907** |    **6312 B** |
|  QuickLinqDelegate |   Int32[500] |    32,107.4 ns |   625.91 ns |   992.76 ns |       - |         - |
|    QuickLinqStruct |   Int32[500] |    21,221.2 ns |   148.33 ns |   131.49 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    28,735.1 ns |   433.58 ns |   384.35 ns |  0.0305 |      88 B |
|   StructLinqStruct |   Int32[500] |    12,892.0 ns |   155.28 ns |   145.25 ns |       - |         - |

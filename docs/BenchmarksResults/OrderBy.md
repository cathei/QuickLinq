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
|               **Linq** | **Int32[10000]** | **1,174,896.3 ns** | **2,023.26 ns** | **1,793.56 ns** | **54.6875** |  **120313 B** |
|  QuickLinqDelegate | Int32[10000] | 1,364,245.0 ns | 1,194.38 ns |   932.49 ns |       - |       1 B |
|    QuickLinqStruct | Int32[10000] | 1,053,600.7 ns | 2,210.41 ns | 1,959.47 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,208,554.5 ns | 1,768.06 ns | 1,476.41 ns |       - |     411 B |
|   StructLinqStruct | Int32[10000] |   904,006.5 ns | 3,029.22 ns | 2,685.33 ns |       - |     145 B |
|               **Linq** |    **Int32[20]** |       **669.2 ns** |     **1.88 ns** |     **1.57 ns** |  **0.2632** |     **552 B** |
|  QuickLinqDelegate |    Int32[20] |       822.9 ns |     4.15 ns |     3.68 ns |       - |         - |
|    QuickLinqStruct |    Int32[20] |       558.8 ns |     0.97 ns |     0.91 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       706.9 ns |     1.20 ns |     1.12 ns |  0.0420 |      88 B |
|   StructLinqStruct |    Int32[20] |       436.6 ns |     0.72 ns |     0.67 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **32,224.8 ns** |    **53.43 ns** |    **47.36 ns** |  **2.9907** |    **6312 B** |
|  QuickLinqDelegate |   Int32[500] |    36,366.7 ns |   695.31 ns |   682.89 ns |       - |         - |
|    QuickLinqStruct |   Int32[500] |    16,911.8 ns |   101.77 ns |    84.98 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    29,058.5 ns |   289.01 ns |   256.20 ns |  0.0305 |      88 B |
|   StructLinqStruct |   Int32[500] |    12,582.9 ns |    26.83 ns |    23.78 ns |       - |         - |

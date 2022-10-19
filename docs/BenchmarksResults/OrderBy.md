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
|               **Linq** | **Int32[10000]** | **1,169,670.6 ns** | **2,655.46 ns** | **2,073.21 ns** | **54.6875** |  **120313 B** |
|  QuickLinqDelegate | Int32[10000] | 1,191,244.1 ns | 1,283.57 ns | 1,200.65 ns |       - |       1 B |
|    QuickLinqStruct | Int32[10000] |   953,951.2 ns | 6,482.69 ns | 6,063.91 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,211,650.2 ns | 6,487.06 ns | 6,068.00 ns |       - |     346 B |
|   StructLinqStruct | Int32[10000] |   899,407.0 ns | 5,142.46 ns | 4,558.66 ns |       - |     161 B |
|               **Linq** |    **Int32[20]** |       **672.6 ns** |     **3.64 ns** |     **3.41 ns** |  **0.2632** |     **552 B** |
|  QuickLinqDelegate |    Int32[20] |       746.5 ns |     1.34 ns |     1.18 ns |       - |         - |
|    QuickLinqStruct |    Int32[20] |       500.6 ns |     2.01 ns |     1.78 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       704.8 ns |     2.50 ns |     2.21 ns |  0.0420 |      88 B |
|   StructLinqStruct |    Int32[20] |       436.3 ns |     0.80 ns |     0.74 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **32,826.0 ns** |   **161.34 ns** |   **150.92 ns** |  **2.9907** |    **6312 B** |
|  QuickLinqDelegate |   Int32[500] |    31,543.4 ns |   336.93 ns |   315.17 ns |       - |         - |
|    QuickLinqStruct |   Int32[500] |    13,679.1 ns |   217.96 ns |   203.88 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    30,457.9 ns |   598.95 ns |   819.85 ns |  0.0305 |      88 B |
|   StructLinqStruct |   Int32[500] |    12,586.9 ns |    60.12 ns |    53.29 ns |       - |         - |

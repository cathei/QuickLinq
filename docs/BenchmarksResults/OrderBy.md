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
|             Method |         list |           Mean |        Error |      StdDev |    Gen0 | Allocated |
|------------------- |------------- |---------------:|-------------:|------------:|--------:|----------:|
|               **Linq** | **Int32[10000]** | **1,175,983.0 ns** |  **4,439.83 ns** | **4,153.02 ns** | **54.6875** |  **120313 B** |
|  QuickLinqDelegate | Int32[10000] | 1,404,731.0 ns | 10,586.86 ns | 9,902.96 ns |       - |       1 B |
|    QuickLinqStruct | Int32[10000] | 1,147,403.1 ns |  4,966.85 ns | 4,645.99 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,213,016.7 ns |  4,582.92 ns | 4,286.86 ns |       - |     379 B |
|   StructLinqStruct | Int32[10000] |   905,326.7 ns |  8,475.84 ns | 7,928.30 ns |       - |     129 B |
|               **Linq** |    **Int32[20]** |       **662.8 ns** |      **1.21 ns** |     **1.07 ns** |  **0.2632** |     **552 B** |
|  QuickLinqDelegate |    Int32[20] |       821.4 ns |      6.92 ns |     6.47 ns |       - |         - |
|    QuickLinqStruct |    Int32[20] |       587.9 ns |      5.06 ns |     4.49 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       702.6 ns |      2.08 ns |     1.73 ns |  0.0420 |      88 B |
|   StructLinqStruct |    Int32[20] |       437.5 ns |      1.98 ns |     1.65 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **32,218.4 ns** |    **329.34 ns** |   **291.95 ns** |  **2.9907** |    **6312 B** |
|  QuickLinqDelegate |   Int32[500] |    39,741.1 ns |    649.69 ns |   607.72 ns |       - |         - |
|    QuickLinqStruct |   Int32[500] |    20,761.2 ns |    305.04 ns |   270.41 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    27,825.6 ns |    193.20 ns |   171.27 ns |  0.0305 |      88 B |
|   StructLinqStruct |   Int32[500] |    12,537.8 ns |     27.47 ns |    24.35 ns |       - |         - |

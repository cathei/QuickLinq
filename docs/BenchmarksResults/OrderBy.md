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
|               **Linq** | **Int32[10000]** | **1,209,093.7 ns** | **10,731.74 ns** | **10,038.48 ns** | **54.6875** |  **120313 B** |
|  QuickLinqDelegate | Int32[10000] | 1,288,892.5 ns | 13,087.52 ns | 12,242.07 ns |       - |       1 B |
|    QuickLinqStruct | Int32[10000] | 1,110,578.4 ns |  8,771.26 ns |  8,204.64 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,230,767.6 ns |  9,287.28 ns |  8,232.93 ns |       - |     346 B |
|   StructLinqStruct | Int32[10000] |   921,812.1 ns |  6,294.36 ns |  5,887.75 ns |       - |     129 B |
|               **Linq** |    **Int32[20]** |       **675.9 ns** |      **5.52 ns** |      **5.16 ns** |  **0.2632** |     **552 B** |
|  QuickLinqDelegate |    Int32[20] |       734.2 ns |      2.83 ns |      2.65 ns |       - |         - |
|    QuickLinqStruct |    Int32[20] |       639.2 ns |      2.23 ns |      2.08 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       713.0 ns |      3.96 ns |      3.71 ns |  0.0420 |      88 B |
|   StructLinqStruct |    Int32[20] |       439.8 ns |      2.80 ns |      2.48 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **33,052.6 ns** |    **176.64 ns** |    **165.23 ns** |  **2.9907** |    **6312 B** |
|  QuickLinqDelegate |   Int32[500] |    32,616.2 ns |    626.40 ns |    696.24 ns |       - |         - |
|    QuickLinqStruct |   Int32[500] |    21,826.9 ns |    196.54 ns |    183.85 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    28,834.8 ns |    515.00 ns |    481.73 ns |  0.0305 |      88 B |
|   StructLinqStruct |   Int32[500] |    12,839.0 ns |    152.73 ns |    127.54 ns |       - |         - |

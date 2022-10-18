## OrderByKey

### Source
[OrderByKey.cs](../../QuickLinq.Benchmarks/Cases/OrderByKey.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.2130/21H1/May2021Update)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT AVX2


```
|             Method |         list |           Mean |        Error |       StdDev |    Gen0 |   Gen1 | Allocated |
|------------------- |------------- |---------------:|-------------:|-------------:|--------:|-------:|----------:|
|               **Linq** | **Int32[10000]** | **1,371,456.7 ns** | **27,278.36 ns** | **28,012.87 ns** | **13.6719** | **1.9531** |  **120313 B** |
|  QuickLinqDelegate | Int32[10000] | 1,589,903.3 ns | 20,658.34 ns | 18,313.08 ns |       - |      - |       1 B |
|    QuickLinqStruct | Int32[10000] | 1,298,090.7 ns | 23,386.67 ns | 25,994.22 ns |       - |      - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,472,579.0 ns | 28,885.10 ns | 29,662.87 ns |       - |      - |     106 B |
|   StructLinqStruct | Int32[10000] |   941,245.6 ns | 18,537.29 ns | 17,339.79 ns |       - |      - |       1 B |
|               **Linq** |    **Int32[20]** |       **859.6 ns** |      **5.43 ns** |      **5.08 ns** |  **0.0658** |      **-** |     **552 B** |
|  QuickLinqDelegate |    Int32[20] |     1,100.6 ns |     17.44 ns |     16.31 ns |       - |      - |         - |
|    QuickLinqStruct |    Int32[20] |       781.8 ns |     14.76 ns |     13.08 ns |       - |      - |         - |
| StructLinqDelegate |    Int32[20] |     1,009.0 ns |     12.45 ns |     11.03 ns |  0.0114 |      - |     104 B |
|   StructLinqStruct |    Int32[20] |       615.8 ns |      8.53 ns |      7.56 ns |       - |      - |         - |
|               **Linq** |   **Int32[500]** |    **38,456.7 ns** |    **648.74 ns** |    **575.09 ns** |  **0.7324** |      **-** |    **6312 B** |
|  QuickLinqDelegate |   Int32[500] |    47,855.8 ns |    342.96 ns |    320.80 ns |       - |      - |         - |
|    QuickLinqStruct |   Int32[500] |    23,519.9 ns |    441.30 ns |    891.45 ns |       - |      - |         - |
| StructLinqDelegate |   Int32[500] |    42,010.7 ns |    461.68 ns |    409.26 ns |       - |      - |     104 B |
|   StructLinqStruct |   Int32[500] |    15,780.2 ns |    303.70 ns |    372.98 ns |       - |      - |         - |

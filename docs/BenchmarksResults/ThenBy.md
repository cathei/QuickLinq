## ThenBy

### Source
[ThenBy.cs](../../QuickLinq.Benchmarks/Cases/ThenBy.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|            Method |                 list |           Mean |        Error |       StdDev |    Gen0 |    Gen1 |    Gen2 | Allocated |
|------------------ |--------------------- |---------------:|-------------:|-------------:|--------:|--------:|--------:|----------:|
|              **Linq** | **Value(...)0000] [38]** | **1,610,629.1 ns** |  **9,312.72 ns** |  **8,711.13 ns** | **52.7344** | **48.8281** | **48.8281** |  **280490 B** |
| QuickLinqDelegate | Value(...)0000] [38] | 2,011,713.0 ns |  1,757.97 ns |  1,644.41 ns |       - |       - |       - |      67 B |
|   QuickLinqStruct | Value(...)0000] [38] | 1,644,892.2 ns | 15,387.33 ns | 14,393.32 ns |       - |       - |       - |       1 B |
|              **Linq** | **Value(...)&gt;[20] [35]** |       **832.7 ns** |      **7.63 ns** |      **7.14 ns** |  **0.4854** |       **-** |       **-** |    **1016 B** |
| QuickLinqDelegate | Value(...)&gt;[20] [35] |     1,425.1 ns |      5.99 ns |      5.60 ns |  0.0305 |       - |       - |      64 B |
|   QuickLinqStruct | Value(...)&gt;[20] [35] |     1,042.5 ns |      5.34 ns |      4.46 ns |       - |       - |       - |         - |
|              **Linq** | **Value(...)[500] [36]** |    **45,532.6 ns** |    **761.30 ns** |    **712.12 ns** |  **6.8970** |       **-** |       **-** |   **14456 B** |
| QuickLinqDelegate | Value(...)[500] [36] |    65,778.5 ns |    466.11 ns |    436.00 ns |       - |       - |       - |      64 B |
|   QuickLinqStruct | Value(...)[500] [36] |    34,042.3 ns |    142.58 ns |    126.39 ns |       - |       - |       - |         - |

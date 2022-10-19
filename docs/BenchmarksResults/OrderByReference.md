## OrderByReference

### Source
[OrderByReference.cs](../../QuickLinq.Benchmarks/Cases/OrderByReference.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |              list |     Mean |     Error |    StdDev |    Gen0 |    Gen1 | Allocated |
|------------------- |------------------ |---------:|----------:|----------:|--------:|--------:|----------:|
|               Linq | IntWrapper[10000] | 1.638 ms | 0.0076 ms | 0.0071 ms | 72.2656 | 15.6250 |  200313 B |
|  QuickLinqDelegate | IntWrapper[10000] | 2.043 ms | 0.0121 ms | 0.0101 ms |       - |       - |       3 B |
|    QuickLinqStruct | IntWrapper[10000] | 1.486 ms | 0.0059 ms | 0.0055 ms |       - |       - |       1 B |
| StructLinqDelegate | IntWrapper[10000] | 1.888 ms | 0.0069 ms | 0.0064 ms |       - |       - |     731 B |
|   StructLinqStruct | IntWrapper[10000] | 1.078 ms | 0.0102 ms | 0.0095 ms |       - |       - |     659 B |

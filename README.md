# ReadMe

This lib was updated to [nuget](https://www.nuget.org/packages/Onelinetest/1.0.11#readme-body-tab "Download this lib")

## How to use this lib?
Usally I use this code in [RoslynPad](https://roslynpad.net "Download that powerful tool"). 
```C#
#r "nuget: Onelinetest, 1.0.11"     // import
using static Gwang.Test.OneLine;   // using
// use it like this
test(i=>{
    // some test code
});
// or execute testing code Parallel
testc(i=>{
    // some test code
});
// or start a NoGC Test.(It is easier to understand the memory allocation.)
testngc(i=>{
    // some test code
});
// ps.The memory occupied by GC in parallel mode is multiplied.
testcngc(i=>{
    // some test code
});
```    
And it's output just like:
``` 
Normal:
  Single-core runs:   126 million times, Elapsed: 465.9176 milliseconds, Memory usage:      12kb, GC Requested Mem:    2Gb.
  Concurrent runs:    80 million times, Elapsed: 662.4186 milliseconds, Memory usage:     308kb, GC Requested Mem:    2Gb.
Local Culture == 2052:
  单核运行:   1.3亿次, 耗时: 418.0452毫秒, 内存占用:     316kb, GC内存:    2Gb.
  并发运行:  8000万次, 耗时: 624.8550毫秒, 内存占用:     912kb, GC内存:    2Gb.

```
So, it is very simple to use. 
Hope you like it!

Also, give me a star [⭐](https://github.com/wanggangzero/OneLineTest "Github") if possible 🤗.

---
Version history:

|version|contents|
|---|:--|
|1.0.1|The primary simple version.|
|1.0.4|Add an itor number for Action.|
|1.0.8|Add local Culture recognition, 2502 uses Chinese output, other english.|
|1.0.11|Add NoGC mod, it is easier to understand the memory allocation(for example as zeroGC programing).|

----
And [here](https://github.com/wanggangzero/OneLineTest "click to view source code") is All the source code.
 
---
this project follow:

[MIT License](https://github.com/wanggangzero/OneLineTest/blob/main/LICENSE "license")


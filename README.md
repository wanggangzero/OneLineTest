# ReadMe

This lib was updated to [nuget](https://www.nuget.org/packages/Onelinetest/1.0.12#readme-body-tab "Download this lib")

## How to use this lib?
Usally I use this code in [RoslynPad](https://roslynpad.net "Download that powerful tool"). 
```C#
#r "nuget: Onelinetest, 1.0.12"     // import
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
// 方便了解内存分配,但是一般情况下性能会变差.
testngc(i=>{
    // some test code
});
// ps.The memory occupied by GC in parallel mode is multiplied.
// 并行情况下,内存分配可能是翻倍的
testcngc(i=>{
    // some test code
});
```    
And it's output just like:
``` 
Normal:
  SingleCore+WithGC runs:   126 million times, Elapsed: 421.5045 milliseconds, Memory usage:   -1028kb, GC Requested Mem:       6kb.
  Concurrent+NoneGC runs:   126 million times, Elapsed: 135.6544 milliseconds, Memory usage:        kb, GC Requested Mem:      72kb.
Local Culture == 2052:
   单核+有GC执行:   1.3亿次, 耗时: 422.3958毫秒, 内存: -1028.00kb, GC内存:   6.00kb.
   并发+无GC执行:   1.3亿次, 耗时: 131.5853毫秒, 内存: 204.00kb, GC内存:  72.00kb.

```
So, it is very simple to use. 
Hope you like it!

Also, give me a star [⭐](https://github.com/wanggangzero/OneLineTest "Github") if possible 🤗.

---
Version history:

|version|contents|
|---|:--|
|1.0.1|最原始的简单版本.<br>The primary simple version.   |
|1.0.4|添加了循环变量.<br>Add an itor number for Action.|
|1.0.8|增加了语言环境识别, 除了2052显示简体中文,其他显示英文.<br>Add local Culture recognition, 2502 uses Chinese output, other english.|
|1.0.11|增加了禁用GC模式(一般情况下性能会变差), 便于了解内存分配(比如zeroGC编程的时候).<br> Add NoGC mod(worse performance), it is easier to understand the memory allocation(for example as zeroGC programing).|
|1.0.12|输出文本小调整.<br>A little Edit the output text.|

----
And [here](https://github.com/wanggangzero/OneLineTest "click to view source code") is All the source code.
 
---
this project follow:

[MIT License](https://github.com/wanggangzero/OneLineTest/blob/main/LICENSE "license")


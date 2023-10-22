# ReadMe

This lib was updated to [nuget](https://www.nuget.org/packages/Onelinetest/1.0.9#readme-body-tab "Download this lib")

## How to use this lib?
Usally I use this code in [RoslynPad](https://roslynpad.net "Download that powerful tool"). 
```C#
#r "nuget: Onelinetest, 1.0.9"     // import
using static Gwang.Test.OneLine;   // using
// use it like this
test(()=>{
    // type here some testing code
});
// or with a itor index (Because one more push stack operation,it may be a little slower, but it's not obvious.)
test(i=>{
    // some test code
});
// or execute testing code Parallel
testc(i=>{
    // some test code
});
// or with concur = true
test(i=>{
    // some test code
},1000,true);
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

----
And [here](https://github.com/wanggangzero/OneLineTest "click to view source code") is All the source code.
 
---
this project follow:

[MIT License](https://github.com/wanggangzero/OneLineTest/blob/main/LICENSE "license")


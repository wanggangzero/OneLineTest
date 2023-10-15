# ReadMe

## How to use this lib?
Usally I use this code in [RoslynPad](https://roslynpad.net "Home page."). 
```C#
#r "nuget: Onelinetest, 1.0.4"     // import
using static Gwang.Test.OneLine;   // using
// use it like this
test(()=>{
    // type here some testing code
    var i=0;
    _= i + (i * i);
}, 10000);
// or 
test(i=>{
    // some code
});
```    
And it's output just like:
``` 
运行1000次,耗时  6.7856毫秒,内存占用：-664kb
```
So, it is very simple to use. 
Hope you like it!

----
And here is All the source code:
```C#
using System.Diagnostics;

/// <summary>
/// 测试方法扩展
/// </summary>
/// <param name="ac">待测逻辑</param>
/// <param name="num">循环次数,(default:1000)</param>
/// <author>wanggangzero@vip.qq.com</author>
public static void test(Action ac, int num = 1000)
{
    var n = num;
    var d1 = DateTime.Now;
    var m1 = Memuse();
    if (null != ac)
    {
        while (num-- > 0)
        {
            ac();
        }
    }
    var m = (Memuse() - m1) / 1024;
    Console.WriteLine($"运行{n}次,耗时{(DateTime.Now - d1).TotalMilliseconds,8}毫秒,内存占用：{m:#.###}kb");
}
/// <summary>
/// 测试方法扩展
/// </summary>
/// <param name="ac">待测逻辑</param>
/// <param name="num">循环次数,(default:1000)</param>
/// <author>wanggangzero@vip.qq.com</author>
public static void test(Action<long> ac, long num = 1000)
{
    var n = num;
    var d1 = DateTime.Now;
    var m1 = Memuse();
    if (null != ac)
    {
        while (num-- > 0)
        {
            ac(num);
        }
    }
    var m = (Memuse() - m1) / 1024;
    Console.WriteLine($"运行{n}次,耗时{(DateTime.Now - d1).TotalMilliseconds,8}毫秒,内存占用：{m:#.###}kb");
}


private static long Memuse()
{
    //获得当前工作进程
    Process proc = Process.GetCurrentProcess();
    return proc.PrivateMemorySize64;
}

```
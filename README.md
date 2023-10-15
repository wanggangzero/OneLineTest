# ReadMe

## How to use this lib?
Usally I use this code in [RoslynPad](https://roslynpad.net "Home page."). 
```C#
#r "nuget: Onelinetest, 1.0.6"     // import
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

Also, give me a star ⭐ if possible 🤗.

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
public static void test(Action ac, long num = 1000)
{
    var d1 = DateTime.Now;
    var m1 = Memuse();
    if (null != ac)
    {
        for (long n = 0; n < num; n++)
        {
            ac();
        }
    }
    var mm = (Memuse() - m1) / 1024;
    var ms = (DateTime.Now - d1).TotalMilliseconds;
    PrintMsg(num, mm, ms);
}
/// <summary>
/// 测试方法扩展
/// </summary>
/// <param name="ac">待测逻辑</param>
/// <param name="num">循环次数,(default:1000)</param>
/// <author>wanggangzero@vip.qq.com</author>

/// <summary>
/// 测试方法扩展
/// </summary>
/// <author>wanggangzero@vip.qq.com</author>
public static void test(Action<long> ac, long num = 1000)
{
    var d1 = DateTime.Now;
    var m1 = Memuse();
    if (null != ac)
    {
        for (long n = 0; n < num; n++)
        {
            ac(n);
        }
    }
    var mm = (Memuse() - m1) / 1024;
    var ms = (DateTime.Now - d1).TotalMilliseconds;
    PrintMsg(num, mm, ms);
}

private static void PrintMsg(long num, long mm, double ms)
{
    var s = num switch
    {
        < 100000000 and >= 10000 => $"{num / 10000f:###.#}万",
        >= 100000000 => $"{num / 100000000f:###.#}亿",
        _ => $"{num:###}",
    };

    var s2 = ms switch
    {
        >= 1000 and < 60000 => $"{ms / 1000:##.###}秒",
        >= 60000 => $"{ms / 60000:##}分{ms % 60000 / 1000:##.###}秒",
        _ => $"{ms:###.#####}毫秒",
    };
    var s3 = mm switch
    {
        > 1024 and < 1024 * 1024 => $"{mm / 1024:#.##}Mb",
        > 1024 * 1024 => $"{mm / 1024 / 1024:#.##}Gb",
        _ => $"{mm:###.###}kb",
    };
    Console.WriteLine($"运行: {s}次, 耗时: {s2}, 内存占用: {s3}.");
}


private static long Memuse()
{
    //获得当前工作进程
    Process proc = Process.GetCurrentProcess();
    return proc.PrivateMemorySize64;
}

```
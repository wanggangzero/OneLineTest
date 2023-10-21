# ReadMe

## How to use this lib?
Usally I use this code in [RoslynPad](https://roslynpad.net "Home page."). 
```C#
#r "nuget: Onelinetest, 1.0.7"     // import
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
单核运行:   1.3亿次, 耗时: 418.0452毫秒, 内存占用:     316kb.
并发运行:   1.3亿次, 耗时: 152.3742毫秒, 内存占用:     884kb.
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
/// <param name="concur">是否并发执行, (default: false)</param>
/// <author>wanggangzero@vip.qq.com</author>
public static void test(Action ac, long num = 1000, bool concur = false)
{

    if (null != ac)
    {

        var d1 = DateTime.Now;
        var m1 = Memuse();
        if (concur)
        {
            Parallel.For(0, num, i => ac());
        }
        else
        {
            long n = 0;
            for (n = 0; n < num; n++)
            {
                ac();
            }
        }
        var mm = (Memuse() - m1) / 1024;
        var ms = (DateTime.Now - d1).TotalMilliseconds;
        PrintMsg(num, mm, ms, concur);
    }
    else
    {
        PrintMsg(0, 0, 0, concur);
    }

}
/// <summary>
/// 测试方法扩展
/// </summary>
/// <param name="ac">待测逻辑</param>
/// <param name="num">循环次数,(default:1000)</param>
/// <param name="concur">是否并发执行, (default: false)</param>
/// <author>wanggangzero@vip.qq.com</author>
public static void test(Action<long> ac, long num = 1000, bool concur = false)
{

    if (null != ac)
    {
        var d1 = DateTime.Now;
        var m1 = Memuse();
        long n = 0;
        if (concur)
        {
            Parallel.For(0, num, ac);
        }
        else
        {
            for (n = 0; n < num; n++)
            {
                ac(n);
            }
        }
        var mm = (Memuse() - m1) / 1024;
        var ms = (DateTime.Now - d1).TotalMilliseconds;
        PrintMsg(num, mm, ms, concur);
    }
    else
    {
        PrintMsg(0, 0, 0, concur);
    }
}

/// <summary>
/// 测试方法扩展(多核并发执行)
/// </summary>
/// <param name="ac">待测逻辑</param>
/// <param name="num">循环次数,(default:1000)</param>
/// <author>wanggangzero@vip.qq.com</author>
public static void testc(Action ac, long num = 1000) => test(ac, num, true);

/// <summary>
/// 测试方法扩展(多核并发执行)
/// </summary>
/// <param name="ac">待测逻辑</param>
/// <param name="num">循环次数,(default:1000)</param>
/// <author>wanggangzero@vip.qq.com</author>
public static void testc(Action<long> ac, long num = 1000) => test(ac, num, true);

private static void PrintMsg(long num, long mm, double ms, bool concur = false)
{
    var s = num switch
    {
        < 100000000 and >= 10000 => $"{num / 10000f,5:###.#}万",
        >= 100000000 => $"{num / 100000000f,5:###.#}亿",
        _ => $"{num,6:###}",
    };

    var s2 = ms switch
    {
        >= 1000 and < 60000 => $"{ms / 1000,9:#.000}秒",
        >= 60000 => $"{ms / 60000,3:#}分{ms % 60000 / 1000,6:#.000}秒",
        _ => $"{ms,8:###.0000}毫秒",
    };
    var s3 = mm switch
    {
        > 1024 and < 1024 * 1024 => $"{mm / 1024,4:#.##}Mb",
        > 1024 * 1024 => $"{mm / 1024 / 1024,4:#.##}Gb",
        _ => $"{mm,7:#.#}kb",
    };
    Console.WriteLine($"{(concur ? "并发" : "单核")}运行: {s,6}次, 耗时: {s2,8}, 内存占用: {s3,-8}.");
}

private static long Memuse()
{
    //获得当前工作进程
    Process proc = Process.GetCurrentProcess();
    return proc.PrivateMemorySize64;
}

```
# OneLineTest
C# Fast Test Method

## How to use this lib?
Usally I use this code in [RoslynPad](https://roslynpad.net/ "Home page"). 
```C#
    #r "nuget: Onelinetest, 1.0.1"
    using static Gwang.Test.OneLine;

    test(()=>{
        // type here some testing code
        //
        var i=0;
        _= i + (i * i);
    }, 10000);
```    
And it's output just like:
>运行1000次,耗时  6.7856毫秒,内存占用：-664kb

So, it is very simple to use. 
Hope you like it!

----
And here is All the source code:
```C#
using System.Diagnostics;

/// <summary>
/// 测试方法扩展
/// </summary>
/// <author>wanggangzero@vip.qq.com</author>
public static void test(Action ac, int num = 1000)
{
    var n = num;
    var d1 = DateTime.Now;
    var m1 = Memuse();
    while (num-- > 0)
    {
        if (null != ac)
        {
            ac();
        }
    }
    var m = (Memuse() - m1) / 1024;
    Console.WriteLine(string.Format("运行{0}次,耗时{1,8}毫秒,内存占用：{2:#.###}kb", n, (DateTime.Now - d1).TotalMilliseconds, m));
}

private static long Memuse()
{
    //获得当前工作进程
    Process proc = Process.GetCurrentProcess();
    return proc.PrivateMemorySize64;
}

```

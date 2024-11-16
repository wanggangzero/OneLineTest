using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
namespace Gwang.Test
{
    public class OneLine
    {

        /// <summary>
        /// 测试方法扩展
        /// </summary>
        /// <param name="ac">待测逻辑</param>
        /// <param name="num">循环次数,(default:1000)</param>
        /// <param name="concur">是否并发执行, (default: false)</param>
        /// <param name="noGC">是否关闭GC.Collect执行(default: false)</param>
        /// <author>wanggangzero@vip.qq.com</author>
        public static void test(Action<long> ac, long num = 1000, bool concur = false, bool noGC = false)
        {
            if (null == ac)
            {
                PrintMsg(0, 0, 0, 0, concur);
                return;
            }
            GC.Collect();
            if (noGC)
            {
                GC.TryStartNoGCRegion(uint.MaxValue, true);
            }
            var d1 = DateTime.Now;
            var mu = Memuse();
            if (concur)
            {
                Parallel.For(0, num, ac);
            }
            else
            {
                for (long n = 0; n < num; n++)
                {
                    ac(n);
                }
            }
            var m2 = Memuse();
            var mm = (m2.Item1 - mu.Item1) / 1024;
            var gcms = (m2.Item2 - mu.Item2) / 1024;

            var ms = (DateTime.Now - d1).TotalMilliseconds;
            if (noGC)
            {
                GC.EndNoGCRegion();
            }
            GC.Collect();
            PrintMsg(num, mm, gcms, ms, concur, noGC);

        }

        /// <summary>
        /// 测试方法扩展(单核执行)(无垃圾回收)
        /// </summary>
        /// <param name="ac">待测逻辑</param>
        /// <param name="num">循环次数,(default:1000)</param>
        /// <author>wanggangzero@vip.qq.com</author>
        public static void testngc(Action<long> ac, long num = 1000) => test(ac, num, false, true);

        /// <summary>
        /// 测试方法扩展(多核并发执行)(自动GC回收)
        /// </summary>
        /// <param name="ac">待测逻辑</param>
        /// <param name="num">循环次数,(default:1000)</param>
        /// <author>wanggangzero@vip.qq.com</author>
        public static void testc(Action<long> ac, long num = 1000) => test(ac, num, true, false);
        /// <summary>
        /// 测试方法扩展(多核并发执行)(无GC回收操作)
        /// </summary>
        /// <param name="ac">待测逻辑</param>
        /// <param name="num">循环次数,(default:1000)</param>
        /// <author>wanggangzero@vip.qq.com</author>
        public static void testcngc(Action<long> ac, long num = 1000) => test(ac, num, true, true);
        private static void PrintMsgZh(long num, long mm, long gcms, double ms, bool concur = false, bool ngc = false)
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
                _ => $"{ms,8:#0.0000}毫秒",
            };
            var s3 = mm switch
            {
                > 1024 and < 1024 * 1024 => $"{mm / 1024,4:#.##}Mb",
                > 1024 * 1024 => $"{mm / 1024 / 1024,4:#.##}Gb",
                _ => $"{mm,6:#0.00}kb",
            };
            var s4 = gcms switch
            {
                > 1024 and < 1024 * 1024 => $"{gcms / 1024,6:#0.00}Mb",
                > 1024 * 1024 => $"{gcms / 1024 / 1024,6:#0.00}Gb",
                _ => $"{gcms,6:#0.00}kb",
            };
            var str = $"执行: {s,6}次, 耗时: {s2,8}, 内存: {s3,0000000}, GC内存: {s4}.";
            var title = $"{(concur ? "并发" : "单核"),3}+{(ngc ? "无GC" : "有GC"),3}";

           if (IsTypePresent("RoslynPad.Runtime", "RoslynPad.Runtime.ObjectExtensions", out var ext))
    		{
    			var md = ext?.GetMethods().First(mi => mi.Name == "Dump" && mi.IsGenericMethod)?.MakeGenericMethod(typeof(string));
    			md?.Invoke(null, new object[] { str, title, 4, 1, 10000, 10000 });
    		}
    		else if (IsTypePresent("LINQPad.Runtime", "LINQPad.Extensions", out var ext1))
    		{
    			var md = ext1?.GetMethods().First(mi => mi.Name == "Dump"
    			&& mi.IsGenericMethod
    			&& mi.GetParameters().Count() == 2
    			)?.MakeGenericMethod(typeof(string));
    			md?.Invoke(title, new object[] { str, title });
    		}
    		else
    		{
    			Console.WriteLine($"{title} {str}");
    		}
        }
        private static void PrintMsgEn(long num, long mm, long gcms, double ms, bool concur = false, bool ngc = false)
        {
            var s = num switch
            {
                < 1000000 and >= 1000 => $"{num / 1000f,5:###.#} thousand",
                < 1000000000 and >= 1000000 => $"{num / 1000000f,5:###.#} million",
                >= 1000000000 => $"{num / 1000000000f,5:###.#} billion",
                _ => $"{num,6:###}",
            };
            var s2 = ms switch
            {
                >= 1000 and < 60000 => $"{ms / 1000,9:#.000} seconds",
                >= 60000 => $"{ms / 60000,3:#} minutes and {ms % 60000 / 1000,6:#.000} seconds",
                _ => $"{ms,8:###.0000} milliseconds",
            };
            var s3 = mm switch
            {
                > 1024 and < 1024 * 1024 => $"{mm / 1024,4:#.##}Mb",
                > 1024 * 1024 => $"{mm / 1024 / 1024,4:#.##}Gb",
                _ => $"{mm,7:#.#}kb",
            };
            var s4 = gcms switch
            {
                > 1024 and < 1024 * 1024 => $"{gcms / 1024,4:#.##}Mb",
                > 1024 * 1024 => $"{gcms / 1024 / 1024,4:#.##}Gb",
                _ => $"{gcms,7:#.#}kb",
            };
            var str = $"runs: {s,6} times, Elapsed: {s2,8}, Memory usage: {s3,5}, GC Requested Mem: {s4,5}.";
            var title = $"{(concur ? "Concurrent" : "SingleCore")}+{(ngc ? "NoneGC" : "WithGC")}";

            if (IsTypePresent("RoslynPad.Runtime", "RoslynPad.Runtime.ObjectExtensions", out var ext))
    		{
    			var md = ext?.GetMethods().First(mi => mi.Name == "Dump" && mi.IsGenericMethod)?.MakeGenericMethod(typeof(string));
    			md?.Invoke(null, new object[] { str, title, 4, 1, 10000, 10000 });
    		}
    		else if (IsTypePresent("LINQPad.Runtime", "LINQPad.Extensions", out var ext1))
    		{
    			var md = ext1?.GetMethods().First(mi => mi.Name == "Dump"
    			&& mi.IsGenericMethod
    			&& mi.GetParameters().Count() == 2
    			)?.MakeGenericMethod(typeof(string));
    			md?.Invoke(title, new object[] { str, title });
    		}
    		else
    		{
    			Console.WriteLine($"{title} {str}");
    		}
        }
        private static void PrintMsg(long num, long mm, long gcmm, double ms, bool concur = false, bool ngc = false)
        {
            switch (CultureInfo.CurrentCulture.LCID)
            {
                case 2052:
                    PrintMsgZh(num, mm, gcmm, ms, concur, ngc);
                    break;
                default:
                    PrintMsgEn(num, mm, gcmm, ms, concur, ngc);
                    break;
            }
        }
        public static bool IsTypePresent(string AssemblyName, string TypeName, out Type supType)
        {
            try
            {
                Assembly asmb = Assembly.Load(new AssemblyName(AssemblyName));
                supType = asmb.GetType(TypeName);
                if (supType != null)
                {
                    try { Activator.CreateInstance(supType); }
                    catch (MissingMethodException) { }
                }
                return supType != null;
            }
            catch
            {
                supType = null;
                return false;
            }

        }
        private static (long, long) Memuse()
        {
            //获得当前工作进程
            Process proc = Process.GetCurrentProcess();

            return (proc.PrivateMemorySize64, GC.GetTotalMemory(false));
        }

    }
}

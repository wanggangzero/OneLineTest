using System;
using System.Diagnostics;
using System.Globalization;
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
                    for (long n = 0; n < num; n++)
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

        private static void PrintMsgZh(long num, long mm, double ms, bool concur = false)
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
        private static void PrintMsgEn(long num, long mm, double ms, bool concur = false)
        {
            var s = num switch
            {
                < 1000000 and >= 1000 => $"{num / 1000f,5:###.#} thousand",
                < 100000000 and >= 1000000 => $"{num / 1000000f,5:###.#} million",
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
            Console.WriteLine($"{(concur ? "Concurrent" : "Single-core")} runs: {s,6} times, Elapsed: {s2,8}, Memory usage: {s3,-8}.");

        }
        private static void PrintMsg(long num, long mm, double ms, bool concur = false)
        {
            switch (CultureInfo.CurrentCulture.LCID)
            {
                case 2052:
                    PrintMsgZh(num, mm, ms, concur);
                    break;
                default:
                    PrintMsgEn(num, mm, ms, concur);
                    break;
            }
        }

        private static long Memuse()
        {
            //获得当前工作进程
            Process proc = Process.GetCurrentProcess();
            return proc.PrivateMemorySize64;
        }

    }
}
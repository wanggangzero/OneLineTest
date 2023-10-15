using System.Diagnostics;

namespace Gwang.Test
{

    public class OneLine
    {


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


    }
}
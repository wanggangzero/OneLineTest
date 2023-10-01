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


    }
}
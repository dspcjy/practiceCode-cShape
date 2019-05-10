using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        int num = 10;//设置当前总票数
        void Ticket()
        {
            while (true)//设置无限循环
            {
                Monitor.Enter(this);//锁定代码块
                if (num > 0)//判断当前票数是否大于0
                {
                    Thread.Sleep(100);//使当前线程休眠100毫秒
                    //票数减1
                    Console.WriteLine(Thread.CurrentThread.Name + "----票数" + num--);
                }
                //上面锁定了代码，但并没有相应的解锁，因此，在线程一进入时，其他线程无法进入
                //所以应该添加解锁线程的代码，代码如下：
                Monitor.Exit(this);
            }
        }
        static void Main(string[] args)
        {
            Program p = new Program();//创建对象，以便调用对象方法
            Thread tA = new Thread(new ThreadStart(p.Ticket));//分别实例化4个线程，并设置名称
            tA.Name = "线程一";
            Thread tB = new Thread(new ThreadStart(p.Ticket));
            tB.Name = "线程二";
            Thread tC = new Thread(new ThreadStart(p.Ticket));
            tC.Name = "线程三";
            Thread tD = new Thread(new ThreadStart(p.Ticket));
            tD.Name = "线程四";
            tA.Start();//分别启动线程
            tB.Start();
            tC.Start();
            tD.Start();
            Console.ReadLine();
        }
    }
}

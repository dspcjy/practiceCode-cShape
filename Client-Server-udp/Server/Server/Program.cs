using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class Program
    {
        //创建UdpClient对象
        static UdpClient udp = new UdpClient();
        static void Main(string[] args)
        {
            //调用UdpClient对象的Connect方法建立默认远程主机
            udp.Connect("127.0.0.1", 888);
            while (true)
            {
                Thread thread = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            
                            //定义一个字节数组，用来存放发送到远程主机的信息
                            Byte[] sendBytes = Encoding.Default.GetBytes("(" + DateTime.Now.ToLongTimeString() + ")节目预报：八点有大型晚会，请收听");
                            Console.WriteLine("(" + DateTime.Now.ToLongTimeString() + ")节目预报：八点有大型晚会，请收听");
                            //调用UdpClient对象的Send方法将UDP数据报发送到远程主机
                            udp.Send(sendBytes, sendBytes.Length);
                            Thread.Sleep(2000);//线程休眠2秒
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                });
                thread.Start();//启动线程
            }
        }
    }
}

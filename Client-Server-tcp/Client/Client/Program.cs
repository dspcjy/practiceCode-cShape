using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {

        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient();//创建一个TcpClient对象，自动分配主机IP地址和端口号  
            //此处出现异常，因为服务器端地址设置为了127.0.0.1，因此客户端要想与服务器端连接，IP地址和端口号必须一致
            //所以需要将下面的IP地址192.168.1.38修改为127.0.0.1
            //另外，在开发程序时，如果出现此类异常，通常都是由于IP地址不能连接或者端口号已占用造成的
            tcpClient.Connect("127.0.0.1", 888);//连接服务器，其IP和端口号为192.168.1.38和888  
            if (tcpClient != null)//判断是否连接成功
            {
                Console.WriteLine("连接服务器成功");
                NetworkStream networkStream = tcpClient.GetStream();//获取数据流
                BinaryReader reader = new BinaryReader(networkStream);//定义流数据读取对象
                BinaryWriter writer = new BinaryWriter(networkStream);//定义流数据写入对象
                string localip="127.0.0.1";//存储本机IP，默认值为127.0.0.1
                IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());//获取所有IP地址
                foreach (IPAddress ip in ips)
                {
                    if (!ip.IsIPv6SiteLocal)//如果不是IPV6地址
                        localip = ip.ToString();//获取本机IP地址
                }
                writer.Write(localip + " 你好服务器，我是客户端");//向服务器发送消息  
                while (true)
                {
                    try
                    {
                        string strReader = reader.ReadString();//接收服务器发送的数据  
                        if (strReader != null)
                        {
                            Console.WriteLine("来自服务器的消息："+strReader);//输出接收的服务器消息
                        }
                    }
                    catch
                    {
                        break;//接收过程中如果出现异常，退出循环  
                    }
                }
            }
            Console.WriteLine("连接服务器失败");
        }
    }
}

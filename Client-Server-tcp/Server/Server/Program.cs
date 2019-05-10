using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main()
        {
            int port = 888;//端口  
            TcpClient tcpClient;//创建TCP连接对象
            IPAddress[] serverIP = Dns.GetHostAddresses("127.0.0.1");//定义IP地址
            IPAddress localAddress = serverIP[0];//IP地址  
            TcpListener tcpListener = new TcpListener(localAddress, port);//监听套接字
            tcpListener.Start(); //开始监听  
            Console.WriteLine("服务器启动成功，等待用户接入…");//输出消息  
            while (true)
            {
                try
                {
                    tcpClient = tcpListener.AcceptTcpClient();//每接收一个客户端则生成一个TcpClient  
                    NetworkStream networkStream = tcpClient.GetStream();//获取网络数据流
                    BinaryReader reader = new BinaryReader(networkStream);//定义流数据读取对象
                    BinaryWriter writer = new BinaryWriter(networkStream);//定义流数据写入对象
                    while (true)
                    {
                        try
                        {
                            string strReader = reader.ReadString();//接收消息
                            string[] strReaders = strReader.Split(new char[] { ' ' });//截取客户端消息
                            Console.WriteLine("有客户端接入，客户IP：" + strReaders[0]);//输出接收的客户端IP地址  
                            Console.WriteLine("来自客户端的消息：" + strReaders[1]);//输出接收的消息  
                            string strWriter = "我是服务器，欢迎光临";//定义服务端要写入的消息
                            writer.Write(strWriter);//向对方发送消息  
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
                catch
                {
                    break;
                }
            }
        }
    }
}

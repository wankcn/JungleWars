using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TCPClient
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            // 客户端不需要绑定ip和端口 只需要和服务器端建立连接
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("10.8.215.46"), 4869));

            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);
            // 调用玩Receive后整个程序会暂停在这里，直到当它接收到服务器信息的时候
            string msg = Encoding.UTF8.GetString(data, 0, count);
            Console.WriteLine(msg);

            for (int i = 0; i < 200; i++)
            {
                // 每次传输数据之前会加上一个长度进行传输
                clientSocket.Send(Message.GetBytes(i.ToString()));
            }


            // 数据循环发送
            // while (true)
            // {
            //     string str = Console.ReadLine(); // 读取用户一行输入传送给服务器端
            //     // 输入c关闭
            //     if (str =="c")
            //     {
            //         clientSocket.Close();
            //         return;
            //     }
            //     clientSocket.Send(Encoding.UTF8.GetBytes(str));
            // }

            // 分包测试
            // string str = "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456" +
            //              "分包测试数据123456分包测试数据123456分包测试数据123456分包测试数据123456";
            // clientSocket.Send(Encoding.UTF8.GetBytes(str));

            // Console.ReadKey(); // 程序终止的太快，方便观察输出
            // clientSocket.Close();
        }
    }
}
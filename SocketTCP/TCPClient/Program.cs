﻿using System;
using System.Net.Sockets;
using System.Net;

namespace TCPClient
{
    internal class Program
    { 
        public static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 客户端不需要绑定ip和端口 只需要和服务器端建立连接
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("10.8.215.46"), 4869));
            
            // 服务器会向客户端发送消息，需要接收消息
            byte[] data = new byte[1024]; // 接收时先定义一个数组
            int count = clientSocket.Receive(data); // 知道数组中前count个是接收到的数据
            // 调用玩Receive后整个程序会暂停在这里，直到当它接收到服务器信息的时候
            string msg = System.Text.Encoding.UTF8.GetString(data, 0, count); // 转换成字符串
            Console.WriteLine(msg);

            // 数据循环发送
            while (true)
            {
                string str = Console.ReadLine(); // 读取用户一行输入传送给服务器端
                // 输入c关闭
                if (str =="c")
                {
                    clientSocket.Close();
                    return;
                }
                clientSocket.Send(System.Text.Encoding.UTF8.GetBytes(str));
            }
            
            // Console.ReadKey(); // 程序终止的太快，方便观察输出
            // clientSocket.Close();
            
        }
    }
} 
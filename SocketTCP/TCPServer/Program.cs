using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

// use class Socket and TCP
namespace TCPServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StartServerAsync();
            // 暂停 防止中断
            Console.ReadKey();
        }

        // 异步 必须是静态 
        static void StartServerAsync()
        {
            // 地址类型;socket类型:Dgram(UDP报文) TCP流Stream;协议
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 申请端口，绑定局域网ip 10.8.215.46(变动的)  127.0.0.1本机
            // IPAddress ipAddress= new IPAddress(new byte[]{10,8,215,46}); // 把ip构造成数组传递过去
            IPAddress ipAddress = IPAddress.Parse("10.8.215.46"); // Parse把一个ip字符串转换成对象传递

            // IpAdress xx.xx.xx.xx IpEedPoint IP地址+终端号
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 4869);

            // 绑定ip和端口号
            serverSocket.Bind(ipEndPoint);

            // 开始监听端口 设置50防止服务器崩溃 设置0表示不限制数量
            serverSocket.Listen(0); // 传递挂起的连接队列的最大长度 

            // 接收客户端信息 程序会暂停，直到有一个客户端连接过来才会继续向下运行
            // Socket clientSocket = serverSocket.Accept(); // 接收一个客户端连接 返回一个Socket用来跟客户端进行通信
            serverSocket.BeginAccept(AcceptCallBack,serverSocket);
            
        }

        // 回调函数 接收到一个客户端连接的时候，要对客户端发起监听
        static void AcceptCallBack(IAsyncResult ar)
        {
            Socket serverSocket = ar.AsyncState as Socket;
            Socket clientSocket = serverSocket.EndAccept(ar);
            // 向客户端发送一条数据
            string msg = "Hello client!你好...";
            byte[] data = Encoding.UTF8.GetBytes(msg); // 按照utf8将一个字符串转换成byte数组
            clientSocket.Send(data); // Send传递byte数组，需要将字符串转换成byte数组 需要使用支持中文的编码
            // 持续不断的接收消息 BeginReceive()开始异步接收数据  偏移从0开始存，1024最大数量，事件方法，
            clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);
            
            // 接收完一个客户端之后，重新调用 继续处理下一个客户端连接
            serverSocket.BeginAccept(AcceptCallBack,serverSocket);
        }

        // 需要一个byte数组存储数据，静态方便内部访问
        static byte[] dataBuffer = new byte[1024];

        // 当服务器端接收到一条消息的时候，就会调用事件方法 如果想再去处理，传递数据的时候可以通过null进行传递
        static void ReceiveCallBack(IAsyncResult ar)
        {
            // 使用try catch处理客户端异常关闭终端
            // 调用完BeginReceive，表示开始监听客户端数据的传递，当它就收到一条客户端数据的时候才会去调用
            // 把clientSocket当成一个参数传递到回调方法里面，通过ar.AsyncState来获取到Socket
            Socket clientSocket = null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int count = clientSocket.EndReceive(ar); // 完成数据的接收  count代表接收了多少个字节的数据
                // 如果没有接收到数据就关闭 
                if (count == 0)
                {
                    clientSocket.Close();
                    return;
                }
                // utf-8进行反编码
                string msg = Encoding.UTF8.GetString(dataBuffer, 0, count);
                Console.WriteLine("从客户端接收到数据：" + msg);
                // 完成回调 一直接收数据
                clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // 只出异常时关闭连接
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
        }

        // 同步方式 
        public void StartServerSync()
        {
            // 地址类型;socket类型:Dgram(UDP报文) TCP流Stream;协议
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 申请端口，绑定局域网ip 10.8.215.46(变动的)  127.0.0.1本机
            // IPAddress ipAddress= new IPAddress(new byte[]{10,8,215,46}); // 把ip构造成数组传递过去
            IPAddress ipAddress = IPAddress.Parse("10.8.215.46"); // Parse把一个ip字符串转换成对象传递

            // IpAdress xx.xx.xx.xx IpEedPoint IP地址+终端号
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 4869);

            // 绑定ip和端口号
            serverSocket.Bind(ipEndPoint);  

            // 开始监听端口 设置50防止服务器崩溃 设置0表示不限制数量
            serverSocket.Listen(0); // 传递挂起的连接队列的最大长度 

            // 接收客户端信息 程序会暂停，直到有一个客户端连接过来才会继续向下运行
            Socket clientSocket = serverSocket.Accept(); // 接收一个客户端连接 返回一个Socket用来跟客户端进行通信

            // 向客户端发送一条数据
            string msg = "Hello client!你好...";
            byte[] data = Encoding.UTF8.GetBytes(msg); // 按照utf8将一个字符串转换成byte数组
            clientSocket.Send(data); // Send传递byte数组，需要将字符串转换成byte数组 需要使用支持中文的编码

            // 接收客户端一条消息 
            byte[] dataBuffer = new byte[1024]; // 接收时先定义一个数组
            int count = clientSocket.Receive(dataBuffer); // 知道数组中前count个是接收到的数据
            string msgReceive = Encoding.UTF8.GetString(dataBuffer, 0, count); // 转换成字符串
            Console.WriteLine(msgReceive);

            Console.ReadKey(); // 程序终止的太快，方便观察输出
            // 关闭服务器端
            clientSocket.Close(); // 关闭与客户端的连接
            serverSocket.Close(); // 关闭服务器自身的连接
        }
    }
}
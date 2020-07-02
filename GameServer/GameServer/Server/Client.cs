// 专门处理与客户端的通信问题

using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace GameServer.Server
{
    public class Client
    {
        // 使用socket进行通信 
        private Socket clientSocket;
        private Server server;
        private Message msg = new Message();

        public Client()
        {
        }

        public Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
        }

        // start listen
        public void Start()
        {
            // buffer offset size 
            clientSocket.BeginReceive(msg.Date, msg.StartIndex, msg.RemainSize,
                SocketFlags.None, ReceiveCallBack, null);
        }

        // 递归函数
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                // 先接收数据的长度
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    // 数据为0时直接断开连接
                    CLose();
                }

                // 不为0时需要处理接收到的数据
                msg.ReadMessage(count);
                // 处理结束后要重新进行接收
                Start();
            }
            catch (Exception e)
            {
                // 捕捉到异常也要断开连接
                Console.WriteLine(e);
                CLose();
            }
        }

        // 断开连接的方法
        private void CLose()
        {
            // 关闭连接
            if (clientSocket != null)
                clientSocket.Close();
            // 将自身连接移除
            server.RemoveCLient(this);
        }
    }
}
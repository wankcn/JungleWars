// 专门处理与客户端的通信问题

using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Common;
using GameServer.Model;
using GameServer.Tool;
using MySql.Data.MySqlClient;

namespace GameServer.Servers
{
    public class Client
    {
        // 使用socket进行通信 
        private Socket clientSocket;
        private Server server;
        private Message msg = new Message();

        // 对数据库的连接
        private MySqlConnection mysqlConn;

        // 
        private User user;
        private Result result;

        public Client()
        {
        }

        public Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
            mysqlConn = ConnHelper.Connect(); // 创建client时就建立连接
        }

        // 用来获取房间信息 将数据组拼成字符串返回        
        public string GetUserData()
        {
            return user.Id+","+ user.Username + "," + result.TotalCount + "," + result.WinCount;
        }
        
        // 提供UserController设置User和Result的方法
        public void SetUserData(User user, Result result)
        {
            this.user = user;
            this.result = result;
        }

        public MySqlConnection MySqlConn => mysqlConn; // get方法

        // start listen
        public void Start()
        {
            // 接收之前判断释放 等于空或者没有与服务器连接的时候 不再进行接收
            if (clientSocket == null || clientSocket.Connected == false)
                return;
            // buffer offset size 异步接收
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize,
                SocketFlags.None, ReceiveCallBack, null);
        }

        // 递归函数
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                // 先接收数据的长度
                if (clientSocket == null || clientSocket.Connected == false)
                    return;
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    // 数据为0时直接断开连接
                    CLose();
                }

                // 不为0时需要处理接收到的数据
                msg.ReadMessage(count, OnProcessMessage); // 提供一个递归函数
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

        // 作为递归函数传递
        private void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }

        // 断开连接的方法
        private void CLose()
        {
            // 客户端关闭时，先关闭与数据库的连接
            ConnHelper.ClosConnection(mysqlConn);
            // 关闭连接
            if (clientSocket != null)
                clientSocket.Close();
            // 将自身连接移除
            server.RemoveCLient(this);
        }

        // 进行响应 数据包装发送
        public void Send(ActionCode actionCode, string data)
        {
            byte[] bytes = Message.PackData(actionCode, data);
            clientSocket.Send(bytes);
        }
    }
}
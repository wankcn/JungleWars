// 启动TCP的server端，开启监听

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

namespace GameServer.Server
{
    public class Server
    {
        private IPEndPoint ipEndPoint;

        private Socket serverSocket;

        // 管理所有客户端
        private List<Client> clientList;

        public Server()
        {
        }

        public Server(string ipStr, int port)
        {
            SetIpAndPort(ipStr, port);
        }

        public void SetIpAndPort(string ipStr, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }

        // 启动Server监听
        public void Start()
        {
            // 地址类型;socket类型:Dgram(UDP报文) TCP流Stream;协议
            serverSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            // 绑定开始监听
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }

        // 处理连接回调方法 
        private void AcceptCallBack(IAsyncResult ar)
        {
            // 创建单独client进行处理 EndAccept()异步接受传入的连接尝试 
            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket, this);
            client.Start();
            clientList.Add(client);
        }

        // 移除client的方法，如果断开连接移除
        public void RemoveCLient(Client client)
        {
            // 锁定之后在进行移除 防止访问异常
            lock (clientList)
            {
                clientList.Remove(client);
            }
        }
    }
}
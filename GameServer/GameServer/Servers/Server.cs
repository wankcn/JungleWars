// 启动TCP的server端，开启监听

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using Common;
using GameServer.Controller;

namespace GameServer.Servers
{
    public class Server
    {
        private IPEndPoint ipEndPoint;

        private Socket serverSocket;

        // 管理所有客户端
        private List<Client> clientList = new List<Client>();

        // 不需要初始化参数，直接new构造
        private ControllerManager controllerManager;

        public Server()
        {
        }

        public Server(string ipStr, int port)
        {
            SetIpAndPort(ipStr, port);
            controllerManager = new ControllerManager(this); // this把当前对象传递过去
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
            serverSocket.BeginAccept(AcceptCallBack, null);
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

        // 给client客户端发起响应
        // 客户端，响应的RequestCode 响应的字符串数据
        // controller处理完请求之后会调用SendResponse
        public void SendResponse(Client client, ActionCode actionCode, string data)
        {
            client.Send(actionCode, data);
        }

        // 用来处理消息的方法
        public void HandleRequest(RequestCode requestCode, ActionCode actionCode,
            string data, Client client)
        {
            // 把方法的调用传递给CcontrollerManager
            // 通过sever中介，Manager与sercer交互 server与client交互 
            controllerManager.HandleRequest(requestCode, actionCode, data, client);
        }
    }
}
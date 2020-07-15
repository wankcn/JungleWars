// 解析服务器端消息，解析的消息转发给BaseRequest进行处理

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using Common;

/// <summary>
/// 用来管理与服务器端socket的连接
/// </summary>
public class ClientManager : BaseManager
{
    // 静态变量 ip地址与端口号
    private const string IP = "127.0.0.1";
    private const int PORT = 4869;

    private Socket clientSocket;
    private Message msg = new Message(); // 数据存储与解析

    public ClientManager(GameFacade facade) : base(facade)
    {
    }

    // 与服务器端建立连接
    // 监听OnInit方法
    public override void OnInit()
    {
        base.OnInit();
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);
            Start();
        }
        catch (Exception e)
        {
            // unity界面的输出用debug：Unable to connect to server, please check network
            Debug.LogWarning("无法连接到服务器端，请检查网络" + e);
        }
    }

    private void Start()
    {
        // 异步消息的接收 msg.RemainSize可接收的最多数据个数
        clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize,
            SocketFlags.None, ReceiveCallBack, null);
    }

    // 递归函数
    private void ReceiveCallBack(IAsyncResult ar)
    {
        // count表示接收到多少字节的数据
        try
        {
            // 接收之前判断释放 等于空或者没有与服务器连接的时候
            if (clientSocket == null || clientSocket.Connected == false)
                return;
            // count收数据的字节长度
            int count = clientSocket.EndReceive(ar);
            msg.ReadMessage(count, OnProcessDataCallBack);
            // 接收数据之后继续监听服务器端数据
            Start();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    // 作为递归函数传递给ReadMessage
    private void OnProcessDataCallBack(ActionCode actionCode, string data)
    {
        // 通过中介者模式调用RequestManager的处理响应方法
        facade.HandleResponse(actionCode, data);
    }

    // 发送请求 在一些request类中进行调用
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        // 先进行数据打包
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        // 将数据发送到服务器端
        clientSocket.Send(bytes);
    }

    // 游戏销毁时候，连接也进行销毁
    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法关闭与服务器端的连接" + e);
        }
    }
}
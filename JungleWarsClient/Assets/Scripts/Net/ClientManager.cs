using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

/// <summary>
/// 用来管理与服务器端socket的连接
/// </summary>
public class ClientManager : BaseManager
{
    // 静态变量 ip地址与端口号
    private const string IP = "127.0.0.1";
    private const int PORT = 4869;

    private Socket clientSocket;

    // 与服务器端建立连接
    // 监听OnInit方法
    public override void OnInit()
    {
        base.OnInit();
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);
        }
        catch (Exception e)
        {
            // unity界面的输出用debug：Unable to connect to server, please check network
            Debug.LogWarning("无法连接到服务器端，请检查网络" + e);
            throw;
        }
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
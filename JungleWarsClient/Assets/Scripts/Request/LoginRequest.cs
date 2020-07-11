using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class LoginRequest : BaseRequest
{
    private LoginPanel loginPanel;

    // 重写父类Awake
    public override void Awake()
    {
        // 初始化在Base.Awake()之前
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();
        base.Awake();
    }

    private void Start()
    {
        
    }

    // 发起登录请求 重写方法 数据的组拼
    public void SendRequest(string username, string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }

    // 重写OnResponse data是服务器端发送的响应数据 数据的解析 具体处理交给LoginPanel 
    public override void OnResponse(string data)
    {
        // data先转int，然后整体强制转换枚举类型
        ReturnCode returnCode = (ReturnCode) int.Parse(data);

        // 通过LoginPanel调用响应方法
        loginPanel.OnLoginResponse(returnCode);
    }
}
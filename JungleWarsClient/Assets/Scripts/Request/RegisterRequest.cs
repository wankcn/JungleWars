using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class RegisterRequest : BaseRequest
{
    private RegisterPanel registerPanel;

    public override void Awake()
    {
        // 初始化在Base.Awake()之前
        requestCode = RequestCode.User;
        actionCode = ActionCode.Register;
        registerPanel = GetComponent<RegisterPanel>();
        base.Awake();
    }

    // 发起注册请求 数据的组拼
    public void SendRequest(string username, string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }

    // 重写OnResponse data是服务器端发送的响应数据 数据的解析 具体处理交给LoginPanel 
    public override void OnResponse(string data)
    {
        // 先得到响应的状态码
        // data先转int，然后整体强制转换枚举类型
        ReturnCode returnCode = (ReturnCode) int.Parse(data);
        registerPanel.OnRegisterResponse(returnCode);
    }
}
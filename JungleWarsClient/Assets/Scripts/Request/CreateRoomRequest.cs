using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class CreateRoomRequest : BaseRequest
{
    private RoomPanel roomPanel; // 用于显示个人信息

    // 在Awake里面设置requestCode和ActionCode
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.CreateRoom;
        base.Awake();
    }

    public void SetPanel( BasePanel panel)
    {
        // 设置值时需要强制转型
        roomPanel = panel as RoomPanel;
    }
    // 用来发起请求
    public override void SendRequest()
    {
        // 传递象征性的数据r防止数据解析的时候出现问题
        base.SendRequest("r");
    }

    // 监听回调
    public override void OnResponse(string data)
    {
        // 成功后需要把个人信息显示在蓝色面板里 红色面板清空等待用户加入
        ReturnCode returnCode = (ReturnCode) int.Parse(data);
        if (returnCode == ReturnCode.Success)
        {
            // UserData ud = facade.GetUserData();
            // roomPanel.SetLocalPlayerResS(ud.Username, ud.TotalCount.ToString(), ud.WinCount.ToString());
            // roomPanel.ClearEnemyPlayerRes();
            roomPanel.SetLocalPlayerResSync();
        }
    }
}
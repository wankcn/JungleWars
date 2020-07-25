using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class CreateRoomRequest : BaseRequest
{
    // 在Awake里面设置requestCode和ActionCode
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.CreateRoom;
        base.Awake();
    }
    
    // 用来发起请求
    public override void SendRequest()
    {
        // 传递r防止数据解析的时候出现问题
        base.SendRequest("r");
    }

    // 监听回调
    public override void OnResponse(string data)
    {
        base.OnResponse(data);
    }
}
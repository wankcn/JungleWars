// 房主退出房间，直接直接销毁房间；非房主的话，将玩家移除，房间还在。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class QuitRoomRequest : BaseRequest {
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.QuitRoom;
        roomPanel = GetComponent<RoomPanel>();
        base.Awake();
    }
    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        // 如果退出成功，返回房间列表
        if (returnCode == ReturnCode.Success)
        {
            roomPanel.OnExitResponse();
        }
    }
}

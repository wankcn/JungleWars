// 处理房间列表的加载

using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class ListRoomRequest : BaseRequest
{
    private RoomListPanel roomListPanel;

    public override void Awake()
    {
        // 指定requestCode
        requestCode = RequestCode.Room;
        actionCode = ActionCode.ListRoom;
        roomListPanel = GetComponent<RoomListPanel>();
        base.Awake();
    }

    // 发起请求 每次进入房间列表界面的时候都需要调用
    public override void SendRequest()
    {
        base.SendRequest("r"); // 避免空串防止解析出错
    }

    // data是服务器端发送来的房间列表信息（玩家信息用逗号分割，房间之间用"｜"分割），根据列表信息加载房间列表 
    public override void OnResponse(string data)
    {
        List<UserData> udList = new List<UserData>();
        if (data != "0")
        {
            string[] udArray = data.Split('|');
            foreach (string ud in udArray)
            {
                string[] strs = ud.Split(',');
                udList.Add(new UserData(int.Parse(strs[0]), strs[1], int.Parse(strs[2]), int.Parse(strs[3])));
            }
        }
        
        roomListPanel.LoadRoomItemSync(udList);
    }
}
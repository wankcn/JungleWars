// 只需要接收响应，不需要发送请求

using Common;

public class UpdateRoomRequest : BaseRequest {
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.UpdateRoom;
        roomPanel = GetComponent<RoomPanel>();
        base.Awake();
    }

    // 重写接收响应没更新房间列表
    public override void OnResponse(string data)
    {
        // date保存的是房间信息
        UserData ud1 = null;
        UserData ud2 = null;
        string[] udStrArray = data.Split('|');
        ud1 = new UserData(udStrArray[0]);
        if(udStrArray.Length>1)
            ud2 = new UserData(udStrArray[1]);
        roomPanel.SetAllPlayerResSync(ud1, ud2);
    }
}

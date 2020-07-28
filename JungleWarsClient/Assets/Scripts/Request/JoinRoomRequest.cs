using Common;

public class JoinRoomRequest : BaseRequest
{
    private RoomListPanel roomListPanel;

    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.JoinRoom;
        roomListPanel = GetComponent<RoomListPanel>();
        base.Awake();
    }

    // 发起加入房间请求
    public void SendRequest(int id)
    {
        base.SendRequest(id.ToString());
    }

    // 处理服务器端响应
    public override void OnResponse(string data)
    {
        string[] strs = data.Split('-');
        // string[] strs2 = strs[0].Split(',');
        // ReturnCode returnCode = (ReturnCode)int.Parse(strs2[0]);
        ReturnCode returnCode = (ReturnCode) int.Parse(strs[0]);
        UserData ud1 = null;
        UserData ud2 = null;
        if (returnCode == ReturnCode.Success)
        {
            string[] udStrArray = strs[1].Split('|');
            ud1 = new UserData(udStrArray[0]);
            ud2 = new UserData(udStrArray[1]);

            // RoleType roleType = (RoleType)int.Parse(strs2[1]);
            // facade.SetCurrentRoleType(roleType);
        }

        roomListPanel.OnJoinResponse(returnCode, ud1, ud2);
    }
}
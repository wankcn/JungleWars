using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class StartGameRequest : BaseRequest
{
    private RoomPanel roomPanel;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.StartGame;
        roomPanel = GetComponent<RoomPanel>();
        base.Awake();
    }

    public override void SendRequest()
    {
        base.SendRequest("r");
    }

    // 成功
    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode) int.Parse(data);
        roomPanel.OnStartResponse(returnCode);
    }
}
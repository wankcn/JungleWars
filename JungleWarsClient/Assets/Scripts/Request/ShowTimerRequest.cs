using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class ShowTimerRequest : BaseRequest {

    private GamePanel gamePanel;
    public override void Awake()
    {
        //requestCode = RequestCode.Game;
        actionCode = ActionCode.ShowTimer;
        gamePanel = GetComponent<GamePanel>();
        base.Awake();
    }

    // 得到Timer
    public override void OnResponse(string data)
    {
        int time = int.Parse(data);
        gamePanel.ShowTimeSync(time); // 异步显示
    }
}
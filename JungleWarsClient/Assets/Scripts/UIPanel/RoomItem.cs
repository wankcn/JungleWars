using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    // 拖拽方式进行赋值
    public Text username;
    public Text totalCount;

    public Text winCount;

    // 加入按钮需要注册按钮的点击事件
    public Button joinButton;

    private int id;
    private RoomListPanel panel;

    // Use this for initialization
    void Start()
    {
        if (joinButton != null)
        {
            // 不为空再进行监听
            joinButton.onClick.AddListener(OnJoinClick);
        }
    }

    // 不确定获取的信息是int还是字符串，重载string和int类型 
    public void SetRoomInfo(int id, string username, int totalCount, int winCount, RoomListPanel panel)
    {
        SetRoomInfo(id, username, totalCount.ToString(), winCount.ToString(), panel);
    }

    public void SetRoomInfo(int id, string username, string totalCount, string winCount, RoomListPanel panel)
    {
        this.id = id;
        this.username.text = username;
        this.totalCount.text = "总场数\n" + totalCount;
        this.winCount.text = "胜利\n" + winCount;
        this.panel = panel;
    }

    // 加入按钮点击事件
    private void OnJoinClick()
    {
        panel.OnJoinClick(id);
    }

    // 销毁自身的方法
    public void DestroySelf()
    {
        // 将自身游戏物体销毁
        GameObject.Destroy(this.gameObject);
    }
}
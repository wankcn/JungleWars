// 控制面板的显示 当登录成功之后

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : BasePanel
{
    // 通过RectTransform控制运动
    private RectTransform battleRes;
    private RectTransform roomList;
    private VerticalLayoutGroup roomLayout;
    private GameObject roomItemPrefab;

    private ListRoomRequest listRoomRequest;

    private CreateRoomRequest createRoomRequest;
    // private JoinRoomRequest joinRoomRequest;

    private List<UserData> udList = null; // 定义集合存储房间信息默认为空

    private void Start()
    {
        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        roomLayout = transform.Find("RoomList/ScrollRect/Layout").GetComponent<VerticalLayoutGroup>();
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject; // 通过资源加载
        transform.Find("RoomList/CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
        transform.Find("RoomList/CreateRoomButton").GetComponent<Button>().onClick.AddListener(OnCreateRoomClick);
        // transform.Find("RoomList/RefreshButton").GetComponent<Button>().onClick.AddListener(OnRefreshClick);
        listRoomRequest = GetComponent<ListRoomRequest>();
        createRoomRequest = GetComponent<CreateRoomRequest>();
        // joinRoomRequest = GetComponent<JoinRoomRequest>();
        EnterAnim();
    }

    public override void OnEnter()
    {
        SetBattleRes();
        // 初始化完成之后才会调用
        if (battleRes != null)
        {
            EnterAnim();
        }

        if (listRoomRequest == null)
        {
            listRoomRequest = GetComponent<ListRoomRequest>();
        }
        listRoomRequest.SendRequest();
    }

    // 新面板加载出来时会调用OnPause 暂停是隐藏列表面板
    public override void OnPause()
    {
        HideAnim();
    }

    public override void OnResume()
    {
        EnterAnim();
        // 页面重新激活刷新列表重新发起一次请求
        listRoomRequest.SendRequest();
    }

    private void Update()
    {
        if (udList != null)
        {
            LoadRoomItem(udList);
            udList = null; // 防止重复加载
        }
    }

    public override void OnExit()
    {
        HideAnim(); // 退出时进行隐藏
    }

    // 注册给Close按钮
    private void OnCloseClick()
    {
        PlayClikSound();
        // 将自身弹出去
        uiMng.PopPanel();
    }

    // 注册创建房间按钮
    private void OnCreateRoomClick()
    {
        uiMng.PushPanel(UIPanelType.Room);
    }

    // 进入的动画
    private void EnterAnim()
    {
        gameObject.SetActive(true); // 启用再播放
        battleRes.localPosition = new Vector3(-1000, 0);
        battleRes.DOLocalMoveX(-563, 0.5f);

        roomList.localPosition = new Vector3(1000, 0);
        roomList.DOLocalMoveX(216, 0.5f);
    }

    // 隐藏的动画
    private void HideAnim()
    {
        battleRes.DOLocalMoveX(-1000, 0.5f);
        // 当前游戏物体进行隐藏
        roomList.DOLocalMoveX(1000, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

    // 添加方法用于设置组件 设置战斗结果
    private void SetBattleRes()
    {
        UserData ud = facade.GetUserData();
        // 对组件进行赋值
        transform.Find("BattleRes/Username").GetComponent<Text>().text = ud.Username;
        transform.Find("BattleRes/TotalCount").GetComponent<Text>().text = "总场数:" + ud.TotalCount;
        transform.Find("BattleRes/WinCount").GetComponent<Text>().text = "胜利:" + ud.WinCount;
    }

    // 提供异步方法供ListRoomRequest访问
    public void LoadRoomItemSync(List<UserData> udList)
    {
        this.udList = udList;
    }

    private void LoadRoomItem(List<UserData> udList)
    {
        // 加载之前先进行清空操作 防止重复加载 得到roomLayout下的所有子物体
        RoomItem[] riArray = roomLayout.GetComponentsInChildren<RoomItem>();
        foreach (RoomItem ri in riArray)
        {
            ri.DestroySelf(); // 所有roomItem现存的物体销毁掉
        }

        int count = udList.Count;
        for (int i = 0; i < count; i++)
        {
            // 实例化后放在布局下
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);
            roomItem.transform.SetParent(roomLayout.transform);
            UserData ud = udList[i];
            roomItem.GetComponent<RoomItem>().SetRoomInfo(ud.Id, ud.Username,
                ud.TotalCount, ud.WinCount, this);
        }

        // 设置房间列表高度
        // 得到子物体的个数
        int roomCount = GetComponentsInChildren<RoomItem>().Length;
        // 得到原有的size
        Vector2 size = roomLayout.GetComponent<RectTransform>().sizeDelta;
        // 房间个数乘以房间自身高度 需要加上房间与房间之间的间隔
        roomLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x,
            roomCount * (roomItemPrefab.GetComponent<RectTransform>().sizeDelta.y + roomLayout.spacing));
    }
    
    // ID 是roomid 房主账号的id是唯一的
    public void OnJoinClick(int id)
    {
        // joinRoomRequest.SendRequest(id);
    }
    

    // 测试加载房间
    // private void Update()
    // {
    //     // 按下鼠标左键的时候 在场景中加载roomItem
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         LoadRoomItem(1);
    //     }
    // }
}
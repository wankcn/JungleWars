using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class RoomPanel : BasePanel
{
    // 本地角色
    private Text localPlayerUsername;
    private Text localPlayerTotalCount;
    private Text localPlayerWinCount;

    // 对战角色
    private Text enemyPlayerUsername;
    private Text enemyPlayerTotalCount;
    private Text enemyPlayerWinCount;

    // 持有对敌我面板，开始，退出按钮的引用
    private Transform bluePanel;
    private Transform redPanel;
    private Transform startButton;
    private Transform exitButton;

    private UserData ud = null;
    private UserData ud1 = null;
    private UserData ud2 = null;

    // 持有request的引用
    private QuitRoomRequest quitRoomRequest;
    private StartGameRequest startGameRequest;

    // 默认不弹出面板
    private bool isPopPanel = false;


    private void Start()
    {
        // 本地玩家与敌方玩家Text初始化
        localPlayerUsername = transform.Find("BluePanel/Username").GetComponent<Text>();
        localPlayerTotalCount = transform.Find("BluePanel/TotalCount").GetComponent<Text>();
        localPlayerWinCount = transform.Find("BluePanel/WinCount").GetComponent<Text>();
        enemyPlayerUsername = transform.Find("RedPanel/Username").GetComponent<Text>();
        enemyPlayerTotalCount = transform.Find("RedPanel/TotalCount").GetComponent<Text>();
        enemyPlayerWinCount = transform.Find("RedPanel/WinCount").GetComponent<Text>();

        // 获取敌我面板，开始，退出按钮的引用
        bluePanel = transform.Find("BluePanel");
        redPanel = transform.Find("RedPanel");
        startButton = transform.Find("StartButton");
        exitButton = transform.Find("ExitButton");

        // 基本信息的方法注册监听
        transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(OnStartClick);
        transform.Find("ExitButton").GetComponent<Button>().onClick.AddListener(OnExitClick);

        quitRoomRequest = GetComponent<QuitRoomRequest>();
        startGameRequest = GetComponent<StartGameRequest>();

        EnterAnim();
    }

    public override void OnEnter()
    {
        // 第一次进入bluePanel为空，只在start方法中调用
        if (bluePanel != null)
            EnterAnim();
        // if (crRequest == null)
        //     crRequest = GetComponent<CreateRoomRequest>();
        // crRequest.SendRequest();
    }

    public override void OnPause()
    {
        ExitAnim();
    }

    public override void OnResume()
    {
        EnterAnim();
    }

    public override void OnExit()
    {
        ExitAnim();
    }

    private void Update()
    {
        // 如果ud不为空进行设置
        if (ud != null)
        {
            SetLocalPlayerRes(ud.Username, ud.TotalCount.ToString(), ud.WinCount.ToString());
            ClearEnemyPlayerRes(); // 红色面板清空等待用户加入
            ud = null; // 设置完之后将ud设置为空，下次异步条件不再执行
        }

        if (ud1 != null)
        {
            SetLocalPlayerRes(ud1.Username, ud1.TotalCount.ToString(), ud1.WinCount.ToString());
            if (ud2 != null)
                SetEnemyPlayerRes(ud2.Username, ud2.TotalCount.ToString(), ud2.WinCount.ToString());
            else  // ud2为空显示等待玩家加入 进行清空
                ClearEnemyPlayerRes();
            ud1 = null;
            ud2 = null;
        }

        if (isPopPanel)
        {
            uiMng.PopPanel();
            isPopPanel = false;
        }
    }

    // 异步方式 
    public void SetLocalPlayerResSync()
    {
        ud = facade.GetUserData();
    }

    public void SetAllPlayerResSync(UserData ud1, UserData ud2)
    {
        this.ud1 = ud1;
        this.ud2 = ud2;
    }

    // 本地角色战绩设置
    public void SetLocalPlayerRes(string username, string totalCount, string winCount)
    {
        localPlayerUsername.text = username;
        localPlayerTotalCount.text = "总场数：" + totalCount;
        localPlayerWinCount.text = "胜利：" + winCount;
    }

    // 敌方玩家战绩设置
    private void SetEnemyPlayerRes(string username, string totalCount, string winCount)
    {
        enemyPlayerUsername.text = username;
        enemyPlayerTotalCount.text = "总场数：" + totalCount;
        enemyPlayerWinCount.text = "胜利：" + winCount;
    }

    // 敌方玩家未加入时需要清空结果 所以数据设置为空
    public void ClearEnemyPlayerRes()
    {
        enemyPlayerUsername.text = "";
        enemyPlayerTotalCount.text = "等待玩家加入....";
        enemyPlayerWinCount.text = "";
    }

    // 开始游戏的监听
    private void OnStartClick()
    {
        startGameRequest.SendRequest();
    } 

    // 退出房间按钮监听
    private void OnExitClick()
    {
        quitRoomRequest.SendRequest();
    }

    public void OnExitResponse()
    {
        isPopPanel = true;
    }

    // 开始游戏响应
    public void OnStartResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Fail)
        {
            uiMng.ShowMessageSync("抱歉，您不是房主，无法开始游戏！！");
        }
        else
        {
            // 切换到游戏面板 异步线程
            uiMng.PushPanelSync(UIPanelType.Game);
            // facade.EnterPlayingSync();
        }
    }

    // 添加显示房间面板的动画
    private void EnterAnim()
    {
        // 进入时先自身启用
        gameObject.SetActive(true);
        // 本地玩家面板从左进入
        bluePanel.localPosition = new Vector3(-1000, 0, 0);
        bluePanel.DOLocalMoveX(-250, 0.4f);
        // 敌方玩家面板从右侧进入
        redPanel.localPosition = new Vector3(1000, 0, 0);
        redPanel.DOLocalMoveX(250, 0.4f);
        // 不显示由0变化到1
        startButton.localScale = Vector3.zero;
        startButton.DOScale(1, 0.4f);
        exitButton.localScale = Vector3.zero;
        exitButton.DOScale(1, 0.4f);
    }

    // 添加隐藏房间面板的动画
    private void ExitAnim()
    {
        // 结束后自身游戏物体禁用
        bluePanel.DOLocalMoveX(-1000, 0.4f);
        redPanel.DOLocalMoveX(1000, 0.4f);
        startButton.DOScale(0, 0.4f);
        exitButton.DOScale(0, 0.4f).OnComplete(() => gameObject.SetActive(false));
    }
}
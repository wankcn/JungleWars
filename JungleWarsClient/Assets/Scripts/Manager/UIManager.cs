using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIManager : BaseManager
{
    // /// 
    // /// 单例模式的核心
    // /// 1，定义一个静态的对象 在外界访问 在内部构造
    // /// 2，构造方法私有化
    //
    // private static UIManager _instance;
    //
    // public static UIManager Instance
    // {
    //     get
    //     {
    //         if (_instance == null)
    //         {
    //             _instance = new UIManager();
    //         }
    //         return _instance;
    //     }
    // }

    private Transform canvasTransform;

    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }

            return canvasTransform;
        }
    }

    private Dictionary<UIPanelType, string> panelPathDict; //存储所有面板Prefab的路径
    private Dictionary<UIPanelType, BasePanel> panelDict; //保存所有实例化面板的游戏物体身上的BasePanel组件
    private Stack<BasePanel> panelStack;
    private MessagePanel msgPanel; // 用来对MessagePanel进行引用
    private UIPanelType panelTypeToPush = UIPanelType.None;

    public UIManager(GameFacade facade) : base(facade)
    {
        ParseUIPanelTypeJson();
    }

    // 重写OnInit加载默认场景
    public override void OnInit()
    {
        base.OnInit();
        PushPanel(UIPanelType.Message); // 消息
        PushPanel(UIPanelType.Start); // 登录按钮
    }

    public override void Update()
    {
        // 当不等于None时是需要加载的
        if (panelTypeToPush != UIPanelType.None)
        {
            PushPanel(panelTypeToPush);
            panelTypeToPush = UIPanelType.None;
        }
    }


    public void PushPanelSync(UIPanelType panelType)
    {
        panelTypeToPush = panelType;
    }

    /// <summary>
    /// 把某个页面入栈，  把某个页面显示在界面上
    /// </summary>
    public void PushPanel(UIPanelType panelType)
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        //判断一下栈里面是否有页面
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);
    }

    /// <summary>
    /// 出栈 ，把页面从界面上移除
    /// </summary>
    public void PopPanel()
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count <= 0) return;

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();
    }

    /// <summary>
    /// 根据面板类型 得到实例化的面板
    /// </summary>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType)
    {
        if (panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        //BasePanel panel;
        //panelDict.TryGetValue(panelType, out panel);//TODO

        BasePanel panel = panelDict.TryGet(panelType);

        if (panel == null)
        {
            //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
            //string path;
            //panelPathDict.TryGetValue(panelType, out path);
            string path = panelPathDict.TryGet(panelType);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            // 加载之后设置属性 得到身上的BasePanel组件设置UiMng 使每一个UI面板都可以访问到UIMng
            instPanel.GetComponent<BasePanel>().UIMng = this;
            instPanel.GetComponent<BasePanel>().Facade = facade;
            panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }
    }

    [Serializable]
    class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList;
    }

    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();

        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");

        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);

        foreach (UIPanelInfo info in jsonObject.infoList)
        {
            //Debug.Log(info.panelType);
            panelPathDict.Add(info.panelType, info.path);
        }
    }

    // 提供赋值的方法 MessagePanel被创建时候进行调用
    public void InjectMsgPanel(MessagePanel msgPanel)
    {
        this.msgPanel = msgPanel;
    }

    // 显示信息 在别的面板想显示信息，直接通过UIManager里的ShowMessage方法即可
    public void ShowMessage(string msg)
    {
        // 安全校验
        if (msgPanel == null)
        {
            Debug.Log("无法显示提示信息，MsgPanel为空");
            return;
        }

        msgPanel.ShowMessage(msg);
    }

    public void ShowMessageSync(string msg)
    {
        if (msgPanel == null)
        {
            Debug.Log("无法显示提示信息，MsgPanel为空");
            return;
        }

        msgPanel.ShowMessageSync(msg);
    }
}
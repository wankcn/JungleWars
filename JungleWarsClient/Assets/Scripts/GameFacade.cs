// 单例模式

using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    private static GameFacade _instance; // 单例

    // 提供get方法供外界调用
    public static GameFacade Instance => _instance;

    private UIManager uiMng;
    private AudioManager audioMng;
    private PlayerManager playerMng;
    private CameraManager cameraMng;
    private RequestManager requestMng;
    private ClientManager clientMng;

    private void Awake()
    {
        // 对instance进行赋值
        if (_instance != null)
        {
            // 说明当前场景已经存在GameFacade
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }

    void Start()
    {
        // 对manager进行初始化
        InitManager();
    }

    void Update()
    {
    }

    // Manger构造与初始化
    private void InitManager()
    {
        uiMng = new UIManager(this);
        audioMng = new AudioManager(this);
        playerMng = new PlayerManager(this);
        cameraMng = new CameraManager(this);
        requestMng = new RequestManager(this);
        clientMng = new ClientManager(this);

        uiMng.OnInit();
        audioMng.OnInit();
        playerMng.OnInit();
        cameraMng.OnInit();
        requestMng.OnInit();
        clientMng.OnInit();
    }

    // 管理的销毁
    private void DestroyManager()
    {
        uiMng.OnDestroy();
        audioMng.OnDestroy();
        playerMng.OnDestroy();
        cameraMng.OnDestroy();
        requestMng.OnDestroy();
        clientMng.OnDestroy();
    }

    // 游戏销毁时候进行监听
    private void OnDestroy()
    {
        DestroyManager();
    }

    // 提供addRequest方法方便BaseRequest调用 降低耦合性
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestMng.AddRequest(actionCode, request);
    }

    // 销毁请求
    public void RemoveRequest(ActionCode actionCode)
    {
        requestMng.RemoveRequest(actionCode);
    }

    // clientmanager 进行响应的处理 中转
    public void HandleResponse(ActionCode actionCode, string data)
    {
        requestMng.HandleResponse(actionCode, data);
    }

    // 提供方法使其他模块调用显示信息
    public void ShowMessage(string msg)
    {
        // 通过UIManager来显示
        uiMng.ShowMessage(msg);
    }

    // 发送请求
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientMng.SendRequest(requestCode, actionCode, data);
    }
    
    // 用于外界切换背景音乐
    public void PlayBgSound(string soundName)
    {
        audioMng.PlayBgSound(soundName);
    }
    
    // 用于外界添加声音
    public void PlayNormalSound(string soundName)
    {
        audioMng.PlayNormalSound(soundName);
    } 
}
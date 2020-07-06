using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    private UIManager uiMng;
    private AudioManager audioMng;
    private PlayerManager playerMng;
    private CameraManager cameraMng;
    private RequestManager requestMng;
    private ClientManager clientMng;

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
}
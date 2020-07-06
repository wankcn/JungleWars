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
        uiMng = new UIManager();
        audioMng = new AudioManager();
        playerMng = new PlayerManager();
        cameraMng = new CameraManager();
        requestMng = new RequestManager();
        clientMng = new ClientManager();

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
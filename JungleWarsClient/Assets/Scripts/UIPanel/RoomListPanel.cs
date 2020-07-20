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

    private void Start()
    {
        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        transform.Find("RoomList/CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
        EnterAnim();
    }

    public override void OnEnter()
    {
        // 初始化完成之后才会调用
        if (battleRes != null)
        {
            EnterAnim();
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
}
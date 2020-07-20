// 控制面板的显示 当登录成功之后

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoomListPanel : BasePanel
{
    // 通过RectTransform控制运动
    private RectTransform battleRes;
    private RectTransform roomList;

    public override void OnEnter()
    {
        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        EnterAnim();
    }

    // 进入的动画
    private void EnterAnim()
    {
        battleRes.localPosition = new Vector3(-1000, 0);
        battleRes.DOLocalMoveX(-563, 0.5f);

        roomList.localPosition = new Vector3(1000, 0);
        roomList.DOLocalMoveX(216, 0.5f);
    }

    // 隐藏的动画
    private void HideAnim()
    {
    }
}
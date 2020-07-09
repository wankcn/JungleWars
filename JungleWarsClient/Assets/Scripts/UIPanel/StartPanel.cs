using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private Button loginButton;
    private Animator btnAnimator; // Animator一直运行，想要控制动画，要先把Animator禁用 

    public override void OnEnter()
    {
        base.OnEnter();
        // 当Panel被实例化出来时，得到Button组件 根据名字查找
        loginButton = transform.Find("LoginButton").GetComponent<Button>();
        btnAnimator = loginButton.GetComponent<Animator>();
        // 注册点击事件
        loginButton.onClick.AddListener(OnLoginClik);
    }

    // 得到按钮并监听按钮的点击
    private void OnLoginClik()
    {
        // 点击时加载登录面板
        uiMng.PushPanel(UIPanelType.Login);
    }

    public override void OnPause()
    {
        base.OnPause();
        // 先禁用Animator
        btnAnimator.enabled = false;
        // 新的面板弹出时候调用 通过动画隐藏 动画调用完后禁用整个游戏物体 如果只禁用button还是会显示
        Tweener tweener = loginButton.transform.DOScale(0, 0.3f);
        tweener.OnComplete(() => loginButton.gameObject.SetActive(false));
    }

    // 当StartPanel上所以面板都被移除之后，会重新调用
    public override void OnResume()
    {
        base.OnResume();
        // 先启用游戏物体，再启用游戏物体身上的组件
        loginButton.gameObject.SetActive(true);
        // 重新激活时，重新设置为true 动画播放完之后启用
        Tweener tweener = loginButton.transform.DOScale(1, 0.3f);
        tweener.OnComplete(() => btnAnimator.enabled = true);
    }
}
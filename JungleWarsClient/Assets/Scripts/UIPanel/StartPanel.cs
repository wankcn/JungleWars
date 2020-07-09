using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public override void OnEnter()
    {
        base.OnEnter();
        // 当Panel被实例化出来时，得到Button组件 根据名字查找
        Button loginButton = transform.Find("LoginButton").GetComponent<Button>();
        // 注册点击事件
        loginButton.onClick.AddListener(OnLoginClik);
    }
    
    // 得到按钮并监听按钮的点击
    private void OnLoginClik()
    {
        // 点击时加载登录面板
        uiMng.PushPanel(UIPanelType.Login);
    }
}
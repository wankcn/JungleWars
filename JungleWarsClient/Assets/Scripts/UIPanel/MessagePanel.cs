using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    // 得到自身的text组件
    private Text text;
    private float showTime = 1; // 控制提示信息显示1s后隐藏
    private string message = null;

    // 重写 在其中进行控制消息异步
    private void Update()
    {
        if (message != null)
        {
            ShowMessage(message);
            message = null;
        }
    }

    // 重写OnEnter 面板被实例化时候会调用OnEnter
    public override void OnEnter()
    {
        base.OnEnter();
        text = GetComponent<Text>();
        // 默认不显示信息
        text.enabled = false;
        uiMng.InjectMsgPanel(this);
    }

    // 再提供一个间接显示的方法处理主线程调用ShowMessage
    public void ShowMessageSync(string msg)
    {
        message = msg;
    }

    // 提供显示提示信息的方法
    public void ShowMessage(string msg)
    {
        // 修改阿尔法 隐藏时Alpha改为了0，在显示之前用0.2s将阿尔法改为1
        text.CrossFadeAlpha(1, 0.2f, false);
        // 把要显示的信息设置到text上
        text.text = msg;
        text.enabled = true; // 显示
        // 1s之后调用Hide方法
        Invoke("Hide", showTime);
    }

    // 用来处理提示信息隐藏
    private void Hide()
    {
        // 0；1时间从1变化到0；不忽略时间的变化（无法监听回调方法）
        text.CrossFadeAlpha(0, 2, false);
    }
}
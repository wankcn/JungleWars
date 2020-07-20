using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    // 得到close按钮
    private Button colseButton;

    // 面板内容相关 在OnEnter进行获取
    private InputField usernameIF;
    private InputField passwordIF;

    private LoginRequest loginRequest;

    private void Start()
    {
        loginRequest = GetComponent<LoginRequest>();

        usernameIF = transform.Find("UsernameLabel/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLabel/PasswordInput").GetComponent<InputField>();
        // 获取关闭按钮 在子物体里，通过transform.Find方法
        colseButton = transform.Find("CloseButton").GetComponent<Button>();
        // 注册关闭方法
        colseButton.onClick.AddListener(OnCloseClick);
        // 通过transform.Find方法查找到登录和注册按钮
        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginClick);
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        EnterAnim();
    }

    // 当界面暂停 需要进行隐藏
    public override void OnPause()
    {
        HideAnim();
    }

    // 面板重新激活的时候
    public override void OnResume()
    {
        // 继续重新显示
        EnterAnim();
    }

    // 禁用 
    public override void OnExit()
    {
        // 移除时隐藏
        HideAnim();
    }

    // 处理close按钮的点击
    private void OnCloseClick()
    {
        PlayClikSound(); // 播放点击声音
        // 关闭时进行移除面板 触发OnExit()
        uiMng.PopPanel();
    }

    // 登录按钮点击事件
    private void OnLoginClick()
    {
        PlayClikSound(); // 播放点击声音
        // 登录时先验证用户名和密码是否为空
        // IsNullOrEmpty 空串或者空对象都会返回true
        bool usernameIsEmpty = string.IsNullOrEmpty(usernameIF.text);
        bool passwordIsEmpty = string.IsNullOrEmpty(passwordIF.text);
        if (usernameIsEmpty)
        {
            uiMng.ShowMessage("用户名不能为空");
        }

        if (passwordIsEmpty)
        {
            uiMng.ShowMessage("密码不能为空");
        }

        if (usernameIsEmpty && passwordIsEmpty)
        {
            uiMng.ShowMessage("用户名和密码不能为空");
        }

        // 发送到服务器端处理
        loginRequest.SendRequest(usernameIF.text, passwordIF.text);
        uiMng.ShowMessage("登录成功");
    }

    //
    public void OnLoginResponse(ReturnCode returnCode)
    {
        // 登录成功
        if (returnCode == ReturnCode.Success)
        {
            // TODO
            uiMng.PushPanelSync(UIPanelType.RoomList);
        }
        else
        {
            uiMng.ShowMessageSync("用户名或密码错误，无法登录，请重新输入！");
        }
    }

    // 注册按钮点击事件
    private void OnRegisterClick()
    {
        PlayClikSound(); // 播放点击声音
        // 弹出注册面板
        uiMng.PushPanel(UIPanelType.Register);
    }

    private void EnterAnim()
    {
        // 进入时启用自身
        gameObject.SetActive(true);
        // 先设置成0，通过descale渐变成1 0.3s
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f);
        // 面板从外面进来 局部位置 结束位置屏幕正中间
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.3f);
    }

    private void HideAnim()
    {
        transform.DOScale(0, 0.3f);
        transform.DOLocalMoveX(1000, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }
}
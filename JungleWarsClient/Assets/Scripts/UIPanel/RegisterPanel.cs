using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    // 监听按钮
    private InputField usernameIF;
    private InputField passwordIF;
    private InputField rePasswordIF;
    private RegisterRequest registerRequest;

    private void Start()
    {
        registerRequest = GetComponent<RegisterRequest>();

        usernameIF = transform.Find("UsernameLabel/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLabel/PasswordInput").GetComponent<InputField>();
        rePasswordIF = transform.Find("RePasswordLabel/RePasswordInput").GetComponent<InputField>();

        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
        // 先设置成0，通过descale渐变成1 0.3s
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f);
        // 面板从外面进来 局部位置 结束位置屏幕正中间
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.3f);
    }

    // 点击注册事件
    private void OnRegisterClick()
    {
        PlayClikSound(); // 播放点击声音
        bool usernameIsEmpty = string.IsNullOrEmpty(usernameIF.text);
        bool passwordIsEmpty = string.IsNullOrEmpty(passwordIF.text);
        bool rePasswordIsEmpty = string.IsNullOrEmpty(rePasswordIF.text);
        // 首先用户名与密码不能为空
        string msg = "";
        if (usernameIsEmpty)
        {
            uiMng.ShowMessage("用户名不能为空");
        }

        else if (passwordIsEmpty)
        {
            uiMng.ShowMessage("密码不能为空");
        }

        else if (passwordIF.text != rePasswordIF.text)
        {
            uiMng.ShowMessage("密码不一致");
        }
        else
        {
            // 发送到服务端进行注册
            // !usernameIsEmpty && !passwordIsEmpty && !rePasswordIsEmpty && passwordIF.text == rePasswordIF.text
            registerRequest.SendRequest(usernameIF.text, passwordIF.text);
        }
    }

    // 解析服务器端响应的状态码
    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            uiMng.ShowMessageSync("注册成功！");
        }
        else
        {
            uiMng.ShowMessageSync("用户名已存在");
        }
    }

    // 点击关闭按钮
    private void OnCloseClick()
    {
        PlayClikSound(); // 播放点击声音
        // 移除时与进入动画相反
        transform.DOScale(0, 0.3f);
        // 移出到目标位置new Vector3(1000, 0, 0)
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.3f);
        // 注册回调 播放完动画之后将自身面板pop出去 lamda表达式
        tweener.OnComplete(() => uiMng.PopPanel());
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }
}
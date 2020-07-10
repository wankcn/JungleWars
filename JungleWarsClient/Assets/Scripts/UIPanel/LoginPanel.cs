using System.Collections;
using System.Collections.Generic;
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
    // private Button loginButton;
    // private Button registerButton;

    public override void OnEnter()
    {
        base.OnEnter();
        // 进入时启用自身
        gameObject.SetActive(true);
        // 先设置成0，通过descale渐变成1 0.3s
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f);
        // 面板从外面进来 局部位置 结束位置屏幕正中间
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.3f);

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

    // 处理close按钮的点击
    private void OnCloseClick()
    {
        // 移除时与进入动画相反
        transform.DOScale(0, 0.3f);
        // 移出到目标位置new Vector3(1000, 0, 0)
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.3f);
        // 注册回调 播放完动画之后将自身面板pop出去 lamda表达式
        tweener.OnComplete(() => uiMng.PopPanel());
    }

    // 登录按钮点击事件
    private void OnLoginClick()
    {
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
            uiMng.ShowMessage("密码不能为空") ;
        }

        if (usernameIsEmpty && passwordIsEmpty)
        {
            uiMng.ShowMessage("用户名和密码不能为空");
        }
        
        // TODO 发送到服务器端处理
    }

    // 注册按钮点击事件
    private void OnRegisterClick()
    {
    }

    // 禁用
    public override void OnExit()
    {
        base.OnExit();
        // 将自身禁用调，节约性能
        gameObject.SetActive(false);
    }
}
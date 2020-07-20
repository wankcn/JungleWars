# ExceptionLog

## UnityEngine.UnityException: 

### 1. GetColor can only be called from the main thread.

**测试客户端发送请求时无法调用ShowMessage**

要修改text里的某一个东西直接调用ShowMessage是没有问题的，但在响应里调用，响应时异步进行处理，ClientManager里有一个ReceiveCallBack，它在别的线程里，并不在Unity的主线程里 ， 要访问Unity的一些组件设置值的时候，必须在Unity的主线程进行访问。ReceiveCallBack是接收服务器端的响应，是单独开的线程来进行处理的不属于主线程，无法调用ShowMessage。



### 2.Load can only be called from the main thread.

LoginPanel登录成功后加载房间列表面板失败

```c#
public void OnLoginResponse(ReturnCode returnCode)
    {
        // 登录成功
        if (returnCode == ReturnCode.Success)
        {
            // 把房间列表加载到界面上
            uiMng.PushPanel(UIPanelType.RoomList);
        }
        else
        {
            uiMng.ShowMessageSync("用户名或密码错误，无法登录，请重新输入！");
        }
    }
```

这里的OnLoginResponse()是在回调，不是在主线程里执行。不管是访问UI的属性，或者通过GameObject实例化某个物体，都只能在主线程里进行。OnLoginResponse是在异步线程里，通过异步的方式push面板。



##  System.ObjectDisposedException

### Cannot access a disposed object

无法访问释放对象，这是因为客户端断开连接以后，此时的服务器端还在接收消息，在接收消息之前将其返回。

```c#
// 接收消息之前判断释放 等于空或者没有与服务器连接的时候 不再进行接收
if (clientSocket == null || clientSocket.Connected == false)
    return;
```



## 登录

### 无法正常显示登录面板

登录面板登录成功显示房间列表，关闭房间列表显示登录面板，重复操作两次后，不会弹出登录面板，而是弹出登录按钮。

在RoomListPanel中，每次进行Push时都会进行OnEnter，显示了两次，OnCloseClick方法就会注册两次，也就是每次点击OnClick，OnCloseClick方法会执行两遍。

```c#
public override void OnEnter()
{
    battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
    roomList = transform.Find("RoomList").GetComponent<RectTransform>();
    transform.Find("RoomList/CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
    EnterAnim();
}
```

```c#
private void OnCloseClick()
{
    PlayClikSound();
    // 将自身弹出去
    uiMng.PopPanel();
}
```

执行两遍的时候先把房间列表面板pop出去，接着也将登录面板pop出去。不应该执行两次，将Start方法进行重写。把OnClick事件放在Start方法里面，保证代码只执行一次。



## NullReferenceException

### Object reference not set to an instance of an object

OnEnter在Start之前执行，对初始化进行判断，初始化完成时再调用显示面板。

```c#
public override void OnEnter()
{
    // 初始化完成之后才会调用
    if (battleRes != null)
    {
        EnterAnim();
    }
}
```
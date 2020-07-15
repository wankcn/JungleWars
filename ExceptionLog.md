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

这里的OnLoginResponse()是在回调，不是在主线程里执行。不管是访问UI的属性，或者通过GameObject实例化某个物体，都只能在主线程里进行。OnLoginResponse是在异步线程里，通过异步的方式push面板
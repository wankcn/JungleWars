// 用来发起请求 很多请求需要访问游戏物体，挂在场景中时会更加方便
// 客户端的每一个Request都可以挂在游戏物体身上: MonoBehaviour
// 当BaseRequest组件被实例化时，要将组件交给requestManager进行管理

using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class BaseRequest : MonoBehaviour
{
    private RequestCode requestCode = RequestCode.None; // 默认无请求

    // 可能在子类中进行重写 重写时需要调用一下父类的Awake，有可能在Base中做初始化工作
    public virtual void Awake()
    {
        // 把自身添加到RequestManager的字典里进行管理
        GameFacade.Instance.AddRequest(requestCode, this);
    }

    // 发起请求
    public virtual void SendRequest()
    {
    }

    // 进行响应 服务器发送来的数据进行处理
    public virtual void OnResponse(string data)
    {
    }

    // 监听销毁的方法
    public virtual void OnDestroy()
    {
        // 调用GameFacade将自身移除
        GameFacade.Instance.RemoveRequest(requestCode);
    }
}
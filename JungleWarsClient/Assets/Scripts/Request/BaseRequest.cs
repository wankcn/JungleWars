// 用来发起请求 很多请求需要访问游戏物体，挂在场景中时会更加方便
// 客户端的每一个Request都可以挂在游戏物体身上: MonoBehaviour

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
    }
}
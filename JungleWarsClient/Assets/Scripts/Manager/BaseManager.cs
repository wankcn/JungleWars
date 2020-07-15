using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager
{
    // 提供一个GameFacade的引用用于各种管理器方便访问GameFacade
    protected GameFacade facade;

    public BaseManager(GameFacade facade)
    {
        this.facade = facade;
    }

    // 创建Manager可重写的虚函数
    // OnInit用来做初始化 生命的开始
    public virtual void OnInit()
    {
    }

    public virtual void Update()
    {
    }

    // OnDestroy() 当场景切换的时候或退出游戏的时候，各个模块可能需要做清理工作 生命的结束
    public virtual void OnDestroy()
    {
    }
}
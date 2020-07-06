using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager
{
    // 创建Manager可重写的虚函数
    // OnInit用来做初始化 生命的开始
    public virtual void OnInit()
    {
    }
    // OnDestroy() 当场景切换的时候或退出游戏的时候，各个模块可能需要做清理工作 生命的结束
    public virtual void OnDestroy()
    {
    }
}

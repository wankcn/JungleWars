// 角色初始化和管理

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager
{
    // 保存用户数据
    private UserData userData;

    public PlayerManager(GameFacade facade) : base(facade)
    {
    }

    public UserData UserData
    {
        // 提供get方法访问数据
        get { return userData; }
        set => userData = value;
    }
}
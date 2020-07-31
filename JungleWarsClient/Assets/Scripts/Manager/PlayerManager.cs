using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class PlayerManager : BaseManager
{
    public PlayerManager(GameFacade facade) : base(facade)
    {
    }

    private UserData userData; // 当前登录用户

    // 使用字典保存当前角色数据 根绝角色类型得到角色数据
    private Dictionary<RoleType, RoleData> roleDataDict = new Dictionary<RoleType, RoleData>();
    
    public UserData UserData
    {
        set { userData = value; }
        get { return userData; }
    }
    

    // 初始化
    private void InitRoleDataDict()
    {
        roleDataDict.Add(RoleType.Blue, new RoleData(RoleType.Blue, "Hunter_BLUE", "Arrow_BLUE"));
        
        roleDataDict.Add(RoleType.Red, new RoleData(RoleType.Red, "Hunter_RED", "Arrow_RED"));
    }

   
}
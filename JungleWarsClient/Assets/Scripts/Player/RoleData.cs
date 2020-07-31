// 角色类型 不需要挂载游戏物体身上

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RoleData
{
    private const string PREFIX_PREFAB = "Prefabs/";

    public RoleType RoleType { get; private set; }
    public GameObject RolePrefab { get; private set; }
    public GameObject ArrowPrefab { get; private set; }


    public RoleData(RoleType roleType, string rolePath, string arrowPath)
    {
        this.RoleType = roleType;
        this.RolePrefab = Resources.Load(PREFIX_PREFAB + rolePath) as GameObject;
        this.ArrowPrefab = Resources.Load(PREFIX_PREFAB + arrowPath) as GameObject;
    }
}
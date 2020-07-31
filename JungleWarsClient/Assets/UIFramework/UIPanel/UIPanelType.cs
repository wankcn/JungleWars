using UnityEngine;
using System.Collections;
using System;


// 加载面板的时候通过枚举类型进行加载
public enum UIPanelType
{
    None,
    Message,
    Start,
    Login,
    Register,
    RoomList,
    Room,
    Game
}
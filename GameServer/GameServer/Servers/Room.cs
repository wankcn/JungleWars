// 每一个room对象代表一个房间 每一个房间里暂且只有两个客户端
// 每个房间有状态，创建出来等待加入，准备状态，准备之后开始状态，战斗状态、结束状态

using System.Collections.Generic;

namespace GameServer.Servers
{
    // 房间的类型
    enum RoomState
    {
        WaitingJoin, // 等待加入
        WaitingBattle, // 等待开始战斗
        Battle,
        End
    }
    public class Room 
    {
        // 存储当前房间里所有的客户端
        private List<Client> clientRoom = new List<Client>();

        // 默认等待加入
        private RoomState state = RoomState.WaitingJoin; 
    }
}
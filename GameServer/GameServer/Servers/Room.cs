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

        private RoomState state = RoomState.WaitingJoin; // 默认等待加入
        private Server server;

        public Room(Server server)
        {
            this.server = server;
        }

        // 房间是否在等待加入
        public bool IsWaitingJoin()
        {
            return state == RoomState.WaitingJoin;
        }
        
        // 添加房间的功能
        public void AddClient(Client client)
        {
            // 添加的第一个默认是房间的创建者，也是房间管理者 开始游戏只能用创建者开始游戏
            clientRoom.Add(client);
        }

        // 得到房主信息 集合中第一个room
        public string GetHouseOwnerData()
        {
            return clientRoom[0].GetUserData();
        }
    }
}
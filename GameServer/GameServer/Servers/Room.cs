// 每一个room对象代表一个房间 每一个房间里暂且只有两个客户端
// 每个房间有状态，创建出来等待加入，准备状态，准备之后开始状态，战斗状态、结束状态

using System;
using System.Collections.Generic;
using System.Text;

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
            // client.HP = MAX_HP;
            clientRoom.Add(client);
            client.Room = this;
            // 判断房间是否满员 更改房间状态
            if (clientRoom.Count>= 2)
            {
                state = RoomState.WaitingBattle;
            }
        }

        // 得到房主信息 集合中第一个room
        public string GetHouseOwnerData()
        {
            return clientRoom[0].GetUserData();
        }

        // 关闭房间 判断是否是房主，是房主关闭，不是房主保留，从client里移除
        public void Close(Client client)
        {
            // 是房主进行移除
            if (client == clientRoom[0])
            {
                server.RemoveRoom(this);
            }
            else
            {
                clientRoom.Remove(client);
            }
        }
        
        public int GetId()
        {
            // 安全校验 取得第一个客户端的userid
            if (clientRoom.Count > 0)
            {
                return clientRoom[0].GetUserId();
            }
            return -1;
        }
        
        // 取得房间里所有人的战绩
        public String GetRoomData()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Client client in clientRoom)
            {
                sb.Append(client.GetUserData() + "|");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1); // 去掉多余"|"
            }
            return sb.ToString();
        }
    }
}
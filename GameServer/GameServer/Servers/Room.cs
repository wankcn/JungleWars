// 每一个room对象代表一个房间 每一个房间里暂且只有两个客户端
// 每个房间有状态，创建出来等待加入，准备状态，准备之后开始状态，战斗状态、结束状态

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Common;

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
        
        // 玩家移除房间
        public void RemoveClient(Client client)
        {
            client.Room = null;
            clientRoom.Remove(client); // 集合移除不再管理

            // 设置房间状态
            if (clientRoom.Count >= 2)
            {
                state = RoomState.WaitingBattle;
            }
            else
            {
                state = RoomState.WaitingJoin;
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
        
        // 广播消息给房间里的其他玩家 当前点击加入的客户端不需要重复发送消息
        public void BroadcastMessage(Client excludeClient,ActionCode actionCode,string data)
        {
            foreach(Client client in clientRoom)
            {
                if (client != excludeClient)
                {
                    server.SendResponse(client, actionCode, data);
                }
            }
        }
        
        // 判断是否是房主
        public bool IsHouseOwner(Client client)
        {
            // 集合第一位
            return client == clientRoom[0];
        }
        
        // 退出房间
        public void QuitRoom(Client client)
        {
            if (client == clientRoom[0])
            {
                Close();
            }
            else
                clientRoom.Remove(client);
        }
        
        // 销毁房间
        public void Close()
        {
            // 遍历所有client把他们对room的引用取消掉
            foreach(Client client in clientRoom)
            {
                client.Room = null;
            }
            // 房间从server中移除
            server.RemoveRoom(this);
        }
        
        // 开始计时器
        public void StartTimer()
        {
            // 启动
            new Thread(RunTimer).Start();
        }
        
        // 线程方法
        private void RunTimer()
        {
            Thread.Sleep(1000); //暂停1s
            for (int i = 3; i > 0; i--)
            {
                // 每次执行把i发送给客户端执行事件 在客户端解析i显示在界面上
                BroadcastMessage(null, ActionCode.ShowTimer, i.ToString());
                Thread.Sleep(1000);
            }
            // 计时结束后开始游戏
            BroadcastMessage(null, ActionCode.StartPlay, "r");
        }
        
        
        // public void TakeDamage(int damage,Client excludeClient)
        // {
        //     bool isDie = false;
        //     foreach (Client client in clientRoom)
        //     {
        //         if (client != excludeClient)
        //         {
        //             if (client.TakeDamage(damage))
        //             {
        //                 isDie = true;
        //             }
        //         }
        //     }
        //     if (isDie == false) return;
        //     //如果其中一个角色死亡，要结束游戏
        //     foreach (Client client in clientRoom)
        //     {
        //         if (client.IsDie())
        //         {
        //             client.UpdateResult(false);
        //             client.Send(ActionCode.GameOver, ((int)ReturnCode.Fail).ToString());
        //         }
        //         else
        //         {
        //             client.UpdateResult(true);
        //             client.Send(ActionCode.GameOver, ((int)ReturnCode.Success).ToString());
        //         }
        //     }
        //     Close();
        // }
    }
}
using System.Text;
using Common;
using GameServer.Servers;
namespace GameServer.Controller
{
    class RoomController:BaseController
    {
        // 构造 交给controllerManager进行管理
        public RoomController()
        {
            requestCode = RequestCode.Room;
        }
        
        // 创建房间
        public string CreateRoom(string data, Client client, Server server)
        {
            server.CreateRoom(client); // 创建房间
            return ((int)ReturnCode.Success).ToString(); // 返回一个创建成功的字符出
                // .ToString()+","+ ((int)RoleType.Blue).ToString();
        }
        
        // 房间列表 通过server取到房间列表 通过room里的list取到client，再通过client取得战绩 返回给客户端
        public string ListRoom(string data, Client client, Server server)
        {
            // 数据不需要读取，用来请求房间列表信息
            StringBuilder sb = new StringBuilder();
            // 遍历房间集合
            foreach(Room room in server.GetRoomList())
            {
                // 先判断房间状态
                if (room.IsWaitingJoin())
                {
                    // 房主信息返回给客户端，组拼字符串
                    sb.Append(room.GetHouseOwnerData()+"|");
                }
            }
            // 空串返回0 客户端判断是否为0  如果是0就是空的房间列表
            if (sb.Length == 0)
            {
                sb.Append("0");
            }
            else // 如果有去除字符串最后的"｜"
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
        // public string JoinRoom(string data, Client client, Server server)
        // {
        //     int id = int.Parse(data);
        //     Room room = server.GetRoomById(id);
        //     if(room == null)
        //     {
        //         return ((int)ReturnCode.NotFound).ToString();
        //     }
        //     else if (room.IsWaitingJoin() == false)
        //     {
        //         return ((int)ReturnCode.Fail).ToString();
        //     }
        //     else
        //     {
        //         room.AddClient(client);
        //         string roomData = room.GetRoomData();//"returncode,roletype-id,username,tc,wc|id,username,tc,wc"
        //         room.BroadcastMessage(client, ActionCode.UpdateRoom, roomData);
        //         return ((int)ReturnCode.Success).ToString() + "," + ((int)RoleType.Red).ToString()+ "-" + roomData;
        //     }
        // }
        // public string QuitRoom(string data, Client client, Server server)
        // {
        //     bool isHouseOwner = client.IsHouseOwner();
        //     Room room = client.Room;
        //     if (isHouseOwner)
        //     {
        //         room.BroadcastMessage(client, ActionCode.QuitRoom, ((int)ReturnCode.Success).ToString());
        //         room.Close();
        //         return ((int)ReturnCode.Success).ToString();
        //     }
        //     else
        //     {
        //         client.Room.RemoveClient(client);
        //         room.BroadcastMessage(client, ActionCode.UpdateRoom, room.GetRoomData());
        //         return ((int)ReturnCode.Success).ToString();
        //     }
        // }
    }
}

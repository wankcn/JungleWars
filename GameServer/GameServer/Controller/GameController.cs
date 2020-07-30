// 处理游戏相关的对战同步
using Common;
using GameServer.Servers;
namespace GameServer.Controller
{
    class GameController:BaseController
    {
        public GameController()
        {
            requestCode = RequestCode.Game;
        }
        
        // 开始游戏
        public string StartGame(string data, Client client, Server server)
        {
            // 判断是否是房主，房主成功，不是房主失败
            if (client.IsHouseOwner())
            {
                Room room =  client.Room;
                room.BroadcastMessage(client, ActionCode.StartGame, ((int)ReturnCode.Success).ToString());
                room.StartTimer();
                return ((int)ReturnCode.Success).ToString();
            }
            else
            {
                return ((int)ReturnCode.Fail).ToString();
            }
        }
    }
}
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
        
    }
}
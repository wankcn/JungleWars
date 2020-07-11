using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    public class UserController:BaseController
    {
        public UserController()
        {
            requestCode = RequestCode.User;
        }
        // 用来处理登录请求 登录方法
        public void Login(string data, Client client, Server server)
        {
             
        }
    }
}
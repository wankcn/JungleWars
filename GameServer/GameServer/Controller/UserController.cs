using System;
using Common;
using GameServer.DAO;
using GameServer.Servers;
using GameServer.Model;

namespace GameServer.Controller
{
    public class UserController : BaseController
    {
        // 创建一个Dao对象
        private UserDAO userDAO = new UserDAO();

        public UserController()
        {
            requestCode = RequestCode.User;
        }

        // 用来处理登录请求 登录方法
        public string Login(string data, Client client, Server server)
        {
            // 把Data进行分割得到用户名和密码 用户名0，密码1
            string[] strs = data.Split(',');
            User user = userDAO.VerifyUser(client.MySqlConn, strs[0], strs[1]);
            // user为空验证失败
            if (user == null)
            {
                // 取得Return所代表的字符串
                // Enum.GetName(typeof(RequestCode), ReturnCode.Fail);
                // 返回失败给客户端 0的字符串
                return ((int) ReturnCode.Fail).ToString();
            }

            return ((int) ReturnCode.Success).ToString();
        }
    }
}
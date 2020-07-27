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
        private ResultDAO resultDAO = new ResultDAO();

        public UserController()
        {
            requestCode = RequestCode.User;
        }

        // 用来处理登录请求 登录方法
        public string Login(string data, Client client, Server server)
        {
            // 把Data进行分割得到用户名和密码 用户名0，密码1
            string[] str = data.Split(',');
            User user = userDAO.VerifyUser(client.MySqlConn, str[0], str[1]);
            // user为空验证失败
            if (user == null)
            {
                // 取得Return所代表的字符串
                // Enum.GetName(typeof(RequestCode), ReturnCode.Fail);
                // 返回失败给客户端 0的字符串
                return ((int) ReturnCode.Fail).ToString();
            }

            // 进行查询
            Result res = resultDAO.GetResultByUserid(client.MySqlConn, user.Id);
            // 成功把战绩和账号保存到client里
            client.SetUserData(user, res);
            // returncode代表状态码 用户名 战绩
            return string.Format("{0},{1},{2},{3}",
                ((int) ReturnCode.Success).ToString(), user.Username, res.TotalCount, res.WinCount);
        }

        // 用来处理注册请求 存在注册失败，不存在注册成功
        public string Register(string data, Client client, Server server)
        {
            string[] str = data.Split(',');
            string username = str[0];
            string password = str[1];
            bool tmp = userDAO.GetUserByUsername(client.MySqlConn, username);
            if (tmp)
                return ((int) ReturnCode.Fail).ToString();
            // 不重复数据库添加注册信息
            userDAO.AddUser(client.MySqlConn, username, password);
            return ((int) ReturnCode.Success).ToString();
        }
    }
}
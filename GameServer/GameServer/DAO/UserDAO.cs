// 工具类，用来与数据库发起请求
// 一个客户端连接持有一个对数据库的连接和对客户端的连接

using System;
using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    public class UserDAO
    {
        // 验证用户 连接数据库需要持有MySqlConn
        public User VerifyUser(MySqlConnection conn, string username, string password)
        {
            MySqlDataReader reader = null;
            try
            {
                string sql = "select * from user where username=@username and password =@password ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);

                reader = cmd.ExecuteReader();
                // 判断是否有数据
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    // string username = reader.GetString("username");
                    // string password = reader.GetString("password");
                    User user = new User(id, username, password);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("验证信息是异常" + e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return null;
        }
    }
}
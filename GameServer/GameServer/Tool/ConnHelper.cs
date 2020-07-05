// 建立与数据库的连接

using System;
using MySql.Data.MySqlClient;

namespace GameServer.Tool
{
    public class ConnHelper
    {
        private const string CONFIG = "database = Game;" +
                                      "datasource = 127.0.0.1;" +
                                      "user = root;" +
                                      "port = 3306;" +
                                      "pwd = APTX4869";

        // 建立连接的方法
        public static MySqlConnection Connect()
        {
            // 创建连接
            MySqlConnection conn = new MySqlConnection(CONFIG);
            // 打开
            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                Console.WriteLine("数据库连接异常:" + e);
                throw;
            }
        }

        // 关闭功能
        public static void ClosConnection(MySqlConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
            }
            else
            {
                Console.WriteLine("MySqlConnection cannot be empty!");
            }
        }
    }
}
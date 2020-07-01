using System;
using MySql.Data.MySqlClient;

namespace MySqlOperation
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string config = "database = Test;" +
                            "datasource = 127.0.0.1;" +
                            "user = root;" +
                            "port = 3306;" +
                            "pwd = APTX4869";
            MySqlConnection conn = new MySqlConnection(config);
            conn.Open();

            #region 操作查询
            // string sql = "select *from user";
            // MySqlCommand cmd = new MySqlCommand(sql, conn);
            // // 对数据进行读取
            // MySqlDataReader reader = cmd.ExecuteReader();
            // while (reader.Read())
            // {
            //     string username = reader.GetString("username");
            //     string password = reader.GetString("password");
            //     Console.WriteLine(username + ":" + password);
            // }
            // reader.Close();
            #endregion
 
            string userName = "liuxiyi";
            string pwd = "liuxiyi";
            string sql = "insert into user set username='"+userName+"', password='"+pwd+"'";
            MySqlCommand cmd = new MySqlCommand(sql , conn);
            cmd.ExecuteNonQuery();
            
            conn.Close();
        }
    }
}
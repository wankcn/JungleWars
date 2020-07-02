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

            #region 查询操作

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

            #region 插入操作

            // string userName = "wr";
            // string pwd = "wr123';delete from user";
            // string sql = "insert into user set username=@un,password=@pwd";
            // MySqlCommand cmd = new MySqlCommand(sql, conn);
            // cmd.Parameters.AddWithValue("un", userName);
            // cmd.Parameters.AddWithValue("pwd", pwd);
            // cmd.ExecuteNonQuery();

            #endregion

            #region 删除操作

            // string sql = "delete from user where id=@id";
            // MySqlCommand cmd = new MySqlCommand(sql, conn);
            // cmd.Parameters.AddWithValue("id", 5);
            // cmd.ExecuteNonQuery();

            #endregion

            #region 更新操作

            // string sql = "update user set password=@pwd where id=4";
            // MySqlCommand cmd = new MySqlCommand(sql, conn);
            // cmd.Parameters.AddWithValue("pwd", "lover");
            // cmd.ExecuteNonQuery();

            #endregion
            conn.Close();
        }
    }
}
// 对Model的Result进行增删改查

using System;
using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    class ResultDAO
    {
        // 根据用户主键得到用户战绩 
        public Result GetResultByUserid(MySqlConnection conn, int userId)
        {
            MySqlDataReader reader = null;
            try
            {
                string sql = "select * from result where userid = @userid";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("userid", userId);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    int totalCount = reader.GetInt32("totalcount");
                    int winCount = reader.GetInt32("wincount");

                    Result res = new Result(id, userId, totalCount, winCount);
                    return res;
                }
                else
                {
                    // 没有读取到数据
                    Result res = new Result(-1, userId, 0, 0);
                    return res;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetResultByUserid的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return null;
        }

        public void UpdateOrAddResult(MySqlConnection conn, Result res)
        {
            try
            {
                MySqlCommand cmd = null;

                if (res.Id <= -1)
                {
                    cmd = new MySqlCommand(
                        "insert into result set totalcount=@totalcount,wincount=@wincount,userid=@userid", conn);
                }
                else
                {
                    cmd = new MySqlCommand(
                        "update result set totalcount=@totalcount,wincount=@wincount where userid=@userid ", conn);
                }

                cmd.Parameters.AddWithValue("totalcount", res.TotalCount);
                cmd.Parameters.AddWithValue("wincount", res.WinCount);
                cmd.Parameters.AddWithValue("userid", res.UserId);
                cmd.ExecuteNonQuery();
                if (res.Id <= -1)
                {
                    Result tempRes = GetResultByUserid(conn, res.UserId);
                    res.Id = tempRes.Id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在UpdateOrAddResult的时候出现异常：" + e);
            }
        }
    }
}
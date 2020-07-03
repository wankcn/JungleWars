// 对接收的消息进行处理 

using System;
using System.Text;

namespace GameServer.Server
{
    public class Message
    {
        // 从客户端读取到数据后存入message进行解析，另一方法解析从data中解析
        private byte[] data = new byte[1024]; // 让最大消息长度小于1024 
        private int dataLength = 0; // 从0开始存 数组里存了多少个字节的数据

        // 提供访问的方法
        public byte[] Date => data;
        public int StartIndex => dataLength;

        // 还剩余的空间
        public int RemainSize => data.Length - dataLength;

        
        /// <summary>
        /// 解析数据
        /// </summary>
        public void ReadMessage( int newDataAmount)
        {
            dataLength += newDataAmount;
            while (true)
            {
                // 小于4数据长度不完整，需要继续接收消息之后进行解析 
                if (dataLength <= 4)
                    return;
                // 先解析长度0-3的4个字节
                int length = BitConverter.ToInt32(data, 0); // 读取固定4字节
                // 读取剩余数据 大于等于时说明数据是完整的
                if (dataLength - 4 >= length)
                {
                    Console.WriteLine(dataLength+":"+length);
                    // 从4号索引开始读取数据
                    string str = Encoding.UTF8.GetString(data, 4, length);
                    Console.WriteLine("解析到一条数据：" + str);
                    // 把后面的数据前移，进行更新
                    Array.Copy(data, length + 4, data,
                        0, dataLength - length - 4);
                    // 更新信息长度 减去已经解析的数据
                    dataLength -= (length + 4);
                }
                else
                {
                    break; // 数据不完整，等待新的数据接收
                }
            }
        }
    }
}

 

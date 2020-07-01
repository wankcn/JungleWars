using System;
using System.Linq;
using System.Text;

namespace TCPClient
{
    // 处理消息的拼接
    public class Message
    {
        // 字符串转换成字节数组，然后加上数据长度
        public static byte[] GetBytes(string data)
        {
            // 数据的长度就是dataBytes数组的大小
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int dataLength = dataBytes.Length;
            // 转换成字节数组
            byte[] lengthBytes = BitConverter.GetBytes(dataLength);
            // 数据长度+数据 组拼成新数组 前四个字节存储数据长度，后面存储数据
            byte[] newBytes = lengthBytes.Concat(dataBytes).ToArray();
            return newBytes;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using Common;

public class Message
{
    // 从客户端读取到数据后存入message进行解析，另一方法解析从data中解析
    private byte[] data = new byte[1024]; // 让最大消息长度小于1024 
    private int dataLength = 0; // 从0开始存 数组里存了多少个字节的数据

    // 提供访问的方法
    public byte[] Data => data;
    public int StartIndex => dataLength;

    // 还剩余的空间
    public int RemainSize => data.Length - dataLength;


    /// <summary>
    /// 解析数据 定义一个事件，指定事件方法的类型
    /// </summary>
    public void ReadMessage(int newDataAmount, Action<ActionCode, string> processDataCallBack)
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
                // Console.WriteLine(dataLength+":"+length);
                // // 从4号索引开始读取数据
                // string str = Encoding.UTF8.GetString(data, 4, length);
                // Console.WriteLine("解析到一条数据：" + str);

                // 先解析ActionCode  只会解析4个数据
                ActionCode actionCode = (ActionCode) BitConverter.ToInt32(data, 4);
                // 数据从8的位置开始 length-8是剩余数据的字节长度
                string str = Encoding.UTF8.GetString(data, 8, length - 4);
                processDataCallBack(actionCode, str);
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

    // // 响应时数据的包装 client将返回的数组发送给客户端
    // public static byte[] PackData(ActionCode actionCode, string data)
    // {
    //     // 转字节数组
    //     byte[] actionCodeBytes = BitConverter.GetBytes((int) actionCode);
    //     byte[] dataBytes = Encoding.UTF8.GetBytes(data);
    //     // 数据长度  requestCodeBytes固定为4 
    //     int dataAmount = 4 + dataBytes.Length;
    //     byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
    //     // 进行组装 Concat一次只能组拼一个数组
    //     byte[] newBytes = dataAmountBytes.Concat(actionCodeBytes).ToArray()
    //         .Concat(dataBytes).ToArray();
    //     return newBytes;
    // }

    // 重载方法
    public static byte[] PackData(RequestCode requestData, ActionCode actionCode, string data)
    {
        // 转字节数组
        byte[] requestCodeBytes = BitConverter.GetBytes((int) requestData);
        byte[] actionCodeBytes = BitConverter.GetBytes((int) actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        // 数据长度  requestCodeBytes固定为4 actionCodeBytes固定长度也为4
        int dataAmount = 8 + dataBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        // 进行组装 Concat一次只能组拼一个数组
        byte[] newBytes = dataAmountBytes.Concat(requestCodeBytes).ToArray()
            .Concat(actionCodeBytes).ToArray()
            .Concat(dataBytes).ToArray();
        return newBytes;
    }
}
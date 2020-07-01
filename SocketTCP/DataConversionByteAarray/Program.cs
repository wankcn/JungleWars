using System;
using System.Text;

namespace DataConversionByteAarray
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // 无法保证转换数据长度 只要固定4个字节  一个汉字3个字节 数字，字符只占1个字节
            // byte[] data = Encoding.UTF8.GetBytes(" 1a 文若"); // 1是一个字符串里面带有字符1

            int count = 10000;
            // 处理值类型，把数据当作值来处理 int类型整数默认4个字节 始终占有4字节
            byte[] data = BitConverter.GetBytes(count);
            foreach (byte b in data)
            {
                Console.Write(b + ":");
            } 

            Console.ReadKey();
        }
    } 
}
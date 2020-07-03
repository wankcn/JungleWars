# JungleWars
Unity3D开发网络游戏，完成后作为明年毕业设计之一

使用工具：MacOS10.15/Unity2019/Rider2020/MySql8.0/Navicat15

## 开发进度

#### 6月29日

完成TCPServer服务端和TCPClient客户端

实现服务端对一个客户端的连接，只能发送接收一条消息

#### 6月30日

使用异步完成了服务端和多个客户端之间的互连

#### 7月1日

1. 解决粘包、分包问题（使客户端传输数据之前都加上数据长度）

2. 测试字节转换 

#### 7月2日

1. 完善了客户端发送数据
2. 完成了服务端解析数据
3. 完成数据库测试
4. 构建游戏分层结构

#### 7月3日

1. 开始编写游戏服务器端GameServer
2. 创建Server类，Client类，Message类
3. 一个controller处理一类请求





## CSDN开发笔记

1.[TCP服务器端与多个客户端连接的C#代码实现](https://blog.csdn.net/wankcn/article/details/107007522)

2.[TCP协议中的粘包分包问题](https://blog.csdn.net/wankcn/article/details/107063186)

3.[字节转换](https://blog.csdn.net/wankcn/article/details/107071861)

4.[粘包、分包问题解决办法](https://editor.csdn.net/md/?articleId=107074772)

5.[C#连接MySql数据库进行测试](https://editor.csdn.net/md/?articleId=107075998)


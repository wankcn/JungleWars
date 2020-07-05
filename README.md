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

1. **开始游戏服务器端GameServer的开发**
2. 创建Server类，Client类，Message类
3. 创建类库Common，开发控制层
4. 客户端多个request对应服务端一个controller，一个request负责发起一个请求并处理这个请求的响应

#### 7月4日

1. 采用中介者模式，创建controllerManager管理所有控制器
2. 通过ControllerManager进行请求的分发处理
3. 客户端请求的响应处理
4. client接收消息后转发给ControllerManager进行管理
5. Message单一职责原则，只负责处理解析消息
6. 完善对消息的处理以及传递给ControllerManager，controller处理完请求之后会有返回值，controllerManager会根据这个返回值判断是否需要给客户端响应。如果不为空，调用server.SendResponse给客户端响应。

#### 7 月5日

1. 完成了ConnHelper对数据库的连接，在客户端与服务端建立连接时创建的client中连接数据库，断连时也先断开数据库。

2. **开始Unity客户端的开发**

3. 导入资源（UIFramework框架和Res游戏资源）
4. 游戏客户端架构分析图



## CSDN开发笔记

1. [TCP服务器端与多个客户端连接的C#代码实现](https://blog.csdn.net/wankcn/article/details/107007522)

2. [TCP协议中的粘包分包问题](https://blog.csdn.net/wankcn/article/details/107063186)

3. [字节转换](https://blog.csdn.net/wankcn/article/details/107071861)

4. [粘包、分包问题解决办法](https://editor.csdn.net/md/?articleId=107074772)

5. [C#连接MySql数据库进行测试](https://editor.csdn.net/md/?articleId=107075998)


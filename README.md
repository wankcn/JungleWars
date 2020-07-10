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

#### 7月6日

1. 分析游戏客户端架构写成文档(对扩展开放，对修改关闭)

2. Scripts-(Manager/Net) 脚本文件分为两类

3. GameFacade放在场景当中，其余Manager不需要挂载在游戏物体上，由GameFacade直接进行管理

4. 修改UIFramework中的UIManager符合现有框架

5. 完善了客户端框架

   以框架为基础开发游戏，开发ui时是登陆与注册页面，需要发起请求，所以先开发网络模块（与客户端的连接，request的管理）

6. 开发ClientManager，完成了与服务器端的连接和关闭

#### 7月7日

1. 为Unity项目更新common.dll动态链接库
2. 完善客户端向服务器端发送消息的功能 Message/ClientManager
3. 完善接收服务器端的消息并解析
4. 修改BaseManager，为它提供一个GameFacade的引用用于各种管理器方便访问GameFacade
5. 创建请求基类BaseRequset，用来发起请求。
6. 完善了Request对象的管理（修改GameFacade单例模式，中介者模式，gameFacade调用RequestManager，BaseRequest调用GameFacade方法）

#### 7月8日

1. 完善了ClientManager处理响应，中介者模式通过facade调用RequestManager的处理响应方法HandleResponse

   **完成了前期准备工作：服务器端架构与客户端架构的开发**

2. **开始游戏逻辑的开发**

3. 控制场景的视野漫游动画作为菜单界面背景

4. 开发登陆按钮

5. 设计登录面板

#### 7月9日

1. 设计注册面板和提示信息面板

2. 创建面板脚本使他们继承自BasePanel

3. 创建面板预制体，修改json文件和枚举类型

4. 开发提示信息模块

   让Message的方法在其他地方可以进行调用：为BasePanel提供可以访问到UIManager的成员uiMng，使每一个UI面板都可以访问到UIManager，UIManger管理所有面板，面板与面板之间不进行交互，减少耦合。

5. 初始化DOTween插件并测试



#### 7月10日

1. 开发游戏开始界面和登录面板进入的动画效果（使用DOTween）
2. 动画优化（点击登录按钮，弹出登录面板，登录按钮消失；关闭登录面板，登录按钮显示，重复此过程）
3. 修复bug（未控制动画的Normal状态，导致登录按钮点击两次以后无法消失，修改Button，在Normal状态下修改Scale为1，使它保持为1的状态）
4. 处理登录请求（在客户端输入用户名和密码，点击登录获取到信息，通过Request发送到服务器端，在服务器端进行校验，正确允许登录，不正确返回错误信息。）
5. 设计用户表和战绩表







## CSDN开发笔记

1. [TCP服务器端与多个客户端连接的C#代码实现](https://blog.csdn.net/wankcn/article/details/107007522)

2. [TCP协议中的粘包分包问题](https://blog.csdn.net/wankcn/article/details/107063186)

3. [字节转换](https://blog.csdn.net/wankcn/article/details/107071861)

4. [粘包、分包问题解决办法](https://editor.csdn.net/md/?articleId=107074772)

5. [C#连接MySql数据库进行测试](https://editor.csdn.net/md/?articleId=107075998)
6. [游戏客户端架构分析](https://blog.csdn.net/wankcn/article/details/107161186)
7. [游戏的数据库设计](https://editor.csdn.net/md/?articleId=107241816)


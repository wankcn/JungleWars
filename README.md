# JungleWars

Unity3D开发网络游戏

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



#### 7月11日

1. 处理客户端LoginPanel登录按钮点击（根据输入错误的用户名和密码反馈不同信息）

2. 修改客户端代码（与解析处理相关的所有RequestCode都改成ActionCode，使一个ActionCode对应一个Request类）

3. 创建LoginRequest类和RegisterRequest类，分别对应ActionCode中的Login和Register

   RequestCode里的User对应UserController

4. 在客户端发送登录请求

   服务器端创建登录请求对应的RequestCode和ActionCode，添加枚举类型，并在LoginPanel的OnLoginClick()增加发送客户端登录信息

5. 创建UserController，完成DAO层与Model层的创建，完成登录信息数据库校验方法

6. 在服务器端发送登录的响应，建立返回类型的枚举类，返回字符串

7.  在客户端处理登录的响应

8. 测试登录流程并处理Bug（从客户端发送请求到服务器端，服务端处理请求给客户端响应，客户端处理响应）

9. 注册面板的显示和隐藏设计



#### 7月12日

1. 完成在客户端发送注册请求
2. 完成在服务器端处理注册请求（判断用户名是否存在，存在则注册失败，不存在注册成功）
3. 在客户端处理注册的响应
4. 开发声音管理器
5. 开发播放控制按钮点击声音（将GameFacade注入到所有面板中，使它持有播放声音的方法，BasePanel父类持有facade对象，中介者模式实现在其他面板调用播放声音的方法）



#### 7月15日

1. 设计房间面板，显示个人信息（创建房间面板空物体，添加imageUi，左边为个人信息，右边为房间列表）

2. 设计房间列表

   添加滚动面板，在滚动面板上添加空物体，使用Vertical Layout Group垂直排序

   设计房间条目 取消Chilld Force Expand的Heigh取消勾选从上往下排列

   为ScrollRect增加mask和image设置与背景色相同，隐藏超出布局的面板

3. 开发房间列表的滚动条和按钮



#### 7月16日

1. 控制房间列表的显示，即登录成功后显示面板
2. 解决Socket关闭后连接问题（接收之前判断释放，clientSocket等于空或者没有与服务器连接的时候，不再进行接收）



#### 7月19日

1. 面板加载各种动画的开发

   调用BeginAccept()修改服务器端进行循环接收客户端消息

   登录成功进入房间列表登录面板进行隐藏

   循环关闭房间列表弹出登录面板

2.  解决页面跳转Bug（登录面板登录成功显示房间列表，关闭房间列表显示登录面板，重复操作两次后，不会弹出登录面板，而是弹出登录按钮）

   把OnClick事件放在Start方法里面，保证代码只执行一次。

3. 修改LoginPanel和RegisterPanel面板的Start方法



#### 7月20日

1. 建立Model-Result类
2. 创建ResultDAO对Result进行增删改查
3. 修改服务器端对登录请求的处理UserController



#### 7月21日

1. 在客户端处理响应保存战绩
2. 在GameFacade添加用户数据的get与set方法，中介者模式。



#### 7月22日

1. 显示个人战绩

2. 创建RoomItem控制一个房间项的显示和点击处理

   将RoomItem制作预制体

   添加RoomItem自身脚本处理自身显示以及按钮点击

3. 动态创建房间列表 

   在RoomList里处理RoomItem的加载，加载到ScrollRect-Layout-VerticalLayoutGroup

   根据房间加载的个数来设置布局的高度

   （Anchor只有在父物体的宽高发生变化的时候，才会对自身产生影响）

   center修改为pivot，pivot设置y为1，占百分之百高度，解决点击加载房间在屏幕正中间的问题，即修改layout的中心点

   center是视觉中心点，pivot是计算坐标的点，是一个比例



#### 7月23日

1. 服务器端创建与房间相关的类与成员
2. 制作房间界面UI



#### 7月24日

1. 开发房间面板的动画行为和按钮监听
2. 控制房间列表和房间面板的切换显示



 





## CSDN开发笔记

1. [TCP服务器端与多个客户端连接的C#代码实现](https://blog.csdn.net/wankcn/article/details/107007522)

2. [TCP协议中的粘包分包问题](https://blog.csdn.net/wankcn/article/details/107063186)

3. [字节转换](https://blog.csdn.net/wankcn/article/details/107071861)

4. [粘包、分包问题解决办法](https://editor.csdn.net/md/?articleId=107074772)

5. [C#连接MySql数据库进行测试](https://editor.csdn.net/md/?articleId=107075998)
6. [游戏客户端架构分析](https://blog.csdn.net/wankcn/article/details/107161186)
7. [游戏的数据库设计](https://editor.csdn.net/md/?articleId=107241816)


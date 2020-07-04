/*
 * 管理所有controller 中介者模式
 * controller交给server进行管理，client接收到消息之后，需要取得controller进行处理
 * client也是sever管理，client可以像server请求controller
 * server相当于中介，使client不直接与controllerManager交互，降低耦合
 * client和其他模块直接使用manager模块会造成耦合性太高
 * controllerManager只让server使用，其他使用先与server交互
 */

using System;
using System.Collections.Generic;
using Common;
using System.Reflection;
using GameServer.Servers; // 反射

namespace GameServer.Controller
{
    public class ControllerManager
    {
        // 使用字典存储 Controller  一个requestcode对应一个controller
        private Dictionary<RequestCode, BaseController> controllerDict
            = new Dictionary<RequestCode, BaseController>();

        private Server server;

        // 构造方法
        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }

        // 初始化所有Controller 根据当前服务器端有哪些Controller 并把它们实例化出来放到字典里
        // 处理请求的时候通过ControllerManager的字典找到对应controller进行处理
        void InitController()
        {
            DefaultController defaultController = new DefaultController();
            controllerDict.Add(defaultController.RequestCode, defaultController);
        }

        // 服务端返回给客户端的数据需要处理
        // 通过requestCode找到 controller actioncode找到 controller里的方法   
        public void HandleRequest(RequestCode requestCode, ActionCode actionCode,
            string data, Client client)
        {
            BaseController controller;
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);
            if (isGet == false)
            {
                // 不成功的话 无法得到请求所对应的处理方法
                Console.WriteLine("无法得到[" + requestCode + "]所对应的controller,无法处理请求");
                return;
            }

            // 处理请求时，通过反射进行调用，
            // actioncode得到方法名，通过反射机制调用controller里的方法
            // 把枚举类型转换成方法
            string methodName = Enum.GetName(typeof(ActionCode), actionCode); // 枚举类型转换成字符串
            // 调用controller内的methodName方法 GetType()取得自身的类型 GetMethod()得到某个方法信息
            MethodInfo mi = controller.GetType().GetMethod(methodName); // mi得到方法信息
            if (mi == null)
            {
                Console.WriteLine("[警告]在controller[" + controller.GetType()
                                                     + "]中没有对于的处理方法：[" + methodName + "]");
                return;
            }

            object[] param = new object[] {data, client, server};
            // 存在的 invoke(指定对象，)在指定对象中调用
            object o = mi.Invoke(controller, param); // 根据这个返回值判断是否需要给客户端响应

            // 当得到参数的时候
            if (o == null || string.IsNullOrEmpty(o as string))
            {
                return;
            }

            // server向客户端发起响应 o转字符串 还要进行byte打包
            server.SendResponse(client, requestCode, o as string);
        }
    }
}
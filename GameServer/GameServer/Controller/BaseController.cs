using Common;

namespace GameServer.Controller
{
    public abstract class BaseController
    {
        // 枚举类型 requestCode animationsCode
        // 提供处理默认的请求 未指定
        private RequestCode requestCode = RequestCode.None;

        
        // 虚函数子类选择实现 根据客户端发送来的数据 已经服务器返回给客户端的数据 
        // 处理默认请求的方法 当没有指定ActionCode时调用
        // data客户端发送来的数据 它的解析交给Message
        // 发送请求之后，服务器端可能给客户端响应即发送数据 stirng返回给客户端的数据
        public virtual string DefaultHandle(string data)
        {
            // string返回null时，表示不需要给客户端返回 不为空，返回数组
            return null;
        }
        public RequestCode RequestCode => requestCode;
    }
}
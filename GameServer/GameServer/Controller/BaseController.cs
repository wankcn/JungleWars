using Common;

namespace GameServer.Controller
{
    public abstract class BaseController
    {
        // 枚举类型 requestCode animationsCode
        // 提供处理默认的请求 未指定
        private RequestCode requestCode = RequestCode.None;
        //  默认处理方法 虚函数子类选择实现
        public virtual void DefaultHandle()
        {
        }
    }
}
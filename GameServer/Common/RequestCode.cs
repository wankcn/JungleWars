namespace Common
{
    // 枚举类型 一个ReuqsetCode对应一个Controller
    // 客户端向服务器端发送请求的时候，在消息里面指定RequestCode，通过它来确定使用哪个Controller来处理  
    public enum RequestCode
    {
        None,
        User, // 登录请求
        Room,
        Game
    }
}
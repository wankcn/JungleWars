# ExceptionLog

### 1.UnityEngine.UnityException: GetColor can only be called from the main thread.

**测试客户端发送请求时无法调用ShowMessage**

要修改text里的某一个东西直接调用ShowMessage是没有问题的，但在响应里调用，响应时异步进行处理，ClientManager里有一个ReceiveCallBack，它在别的线程里，并不在Unity的主线程里 ， 要访问Unity的一些组件设置值的时候，必须在Unity的主线程进行访问。ReceiveCallBack是接收服务器端的响应，是单独开的线程来进行处理的不属于主线程，无法调用ShowMessage。


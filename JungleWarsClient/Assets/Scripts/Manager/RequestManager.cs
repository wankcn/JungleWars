// manager都没有继承自MonoBehaviour，是不能作为组建存在的，需要new
// 作为游戏组件存在，只能挂在游戏物体身上，让unity来创建

using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class RequestManager : BaseManager
{
    public RequestManager(GameFacade facade) : base(facade)
    {
    }

    // 声明字典来存储所有Request对象
    // 通过RequsetCode找到对应的Request对象来发起请求
    private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

    // 添加请求 请求对象
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestDict.Add(actionCode, request);
    }

    // 当场景中某个物体销毁，字典里的request需要进行移除
    public void RemoveRequest(ActionCode actionCode)
    {
        requestDict.Remove(actionCode);
    }

    // 用来进行响应的处理 string 服务器发送来的数据 
    public void HandleResponse(ActionCode actionCode, string data)
    {
        // 根据requestCode在字典中查找到BaseRequest 通过Request里的OnReponse进行处理数据
        BaseRequest request = requestDict.TryGet<ActionCode, BaseRequest>(actionCode);
        if (request == null)
        {
            Debug.LogWarning("无法得到ActionCode[" + actionCode + "]对应的Request类");
            return;
        }

        request.OnResponse(data);
    }
}
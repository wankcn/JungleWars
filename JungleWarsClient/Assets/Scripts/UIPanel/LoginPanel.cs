using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoginPanel : BasePanel
{
    public override void OnEnter()
    {
        base.OnEnter();
        // 先设置成0，通过descale渐变成1 0.3s
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f);
        // 面板从外面进来 局部位置 结束位置屏幕正中间
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.3f);
    }
}
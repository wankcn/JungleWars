using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    // 得到close按钮
    private Button colseButton;

    public override void OnEnter()
    {
        base.OnEnter();
        // 进入时启用自身
        gameObject.SetActive(true);
        // 先设置成0，通过descale渐变成1 0.3s
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f);
        // 面板从外面进来 局部位置 结束位置屏幕正中间
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.3f);

        // 获取关闭按钮 在子物体里，通过transform.Find方法
        colseButton = transform.Find("CloseButton").GetComponent<Button>();
        // 注册关闭方法
        colseButton.onClick.AddListener(OnCloseClick);
    }

    // 处理close按钮的点击
    private void OnCloseClick()
    {
        // 移除时与进入动画相反
        transform.DOScale(0, 0.3f);
        // 移出到目标位置new Vector3(1000, 0, 0)
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.3f);
        // 注册回调 播放完动画之后将自身面板pop出去 lamda表达式
        tweener.OnComplete(() => uiMng.PopPanel());
    }

    // 禁用
    public override void OnExit()
    {
        base.OnExit();
        // 将自身禁用调，节约性能
        gameObject.SetActive(false);
    }
}
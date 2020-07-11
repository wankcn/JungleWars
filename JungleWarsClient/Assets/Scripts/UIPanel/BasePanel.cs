using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour
{
    private GameFacade facade;

    // 提供可以访问到UIManager的成员
    protected UIManager uiMng;

    // 通过set方法进行赋值
    public UIManager UIMng
    {
        set => uiMng = value;
    }

    public GameFacade Facade
    {
        set => facade = value;
    }

    // 播放点击的声音（点击声一样，放在基类）
    protected void PlayClikSound()
    {
        facade.PlayNormalSound(AudioManager.Sound_ButtonClick);
    }

    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {
    }

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {
    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {
    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关系
    /// </summary>
    public virtual void OnExit()
    {
    }
}
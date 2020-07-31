// GameFacade提供PlaySound和PlayNormalSound方法，中介者模式

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager
{
    public AudioManager(GameFacade facade) : base(facade)
    {
    }

    // 持有对所有音乐的引用
    private const string Sound_Prefix = "Sounds/";
    public const string Sound_Alert = "Alert";
    public const string Sound_ArrowShoot = "ArrowShoot";
    public const string Sound_Bg_Fast = "Bg(fast)";
    public const string Sound_Bg_Moderate = "Bg(moderate)";
    public const string Sound_ButtonClick = "ButtonClick";
    public const string Sound_Miss = "Miss";
    public const string Sound_ShootPerson = "ShootPerson";
    public const string Sound_Timer = "Timer";

    private AudioSource bgAudioSource; // 背景声音
    private AudioSource normalAudioSource; // 普通shengyin

    public override void OnInit()
    {
        // 创建播放声音的游戏物体
        GameObject audioSourceGO = new GameObject("AudioSource(GameObject)");
        bgAudioSource = audioSourceGO.AddComponent<AudioSource>();
        normalAudioSource = audioSourceGO.AddComponent<AudioSource>();

        // 默认控制背景声音的播放 Sound_Bg_Moderate 正常速度背景声音
        PlaySound(bgAudioSource, LoadSound(Sound_Bg_Moderate), 0.1f, true);
    }

    // 播放普通声音用于外界播放 通过字符串加载声音 
    public void PlayNormalSound(string soundName)
    {
        PlaySound(normalAudioSource, LoadSound(soundName), 0.5f);
    }

    // 播放背景声音用于外界播放
    public void PlayBgSound(string soundName)
    {
        PlaySound(bgAudioSource, LoadSound(soundName), 0.5f, true);
    }

    // 根据声音的路径加载AudioClip
    private AudioClip LoadSound(string soundsName)
    {
        return Resources.Load<AudioClip>(Sound_Prefix + soundsName);
    }

    // 播放声音的方法 （播放声音的组件，声音源，声音大小，是否循环默认不需要）
    private void PlaySound(AudioSource audioSource, AudioClip clip, float volume, bool loop = false)
    {
        audioSource.clip = clip;
        audioSource.volume = volume; //(0~1) 0最小值 1最大值 
        audioSource.loop = loop;
        audioSource.Play();
    }
}
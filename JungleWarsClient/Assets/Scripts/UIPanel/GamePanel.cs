using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class GamePanel : BasePanel
{
    private Text timer;

    private int time = -1; // 默认不显示
    // private Button successBtn;
    // private Button failBtn;
    // private Button exitBtn;

    // private QuitBattleRequest quitBattleRequest;
    private void Start()
    {
        timer = transform.Find("Timer").GetComponent<Text>();
        timer.gameObject.SetActive(false);
        // successBtn = transform.Find("SuccessButton").GetComponent<Button>();
        // successBtn.onClick.AddListener(OnResultClick);
        // successBtn.gameObject.SetActive(false);
        // failBtn = transform.Find("FailButton").GetComponent<Button>();
        // failBtn.onClick.AddListener(OnResultClick);
        // failBtn.gameObject.SetActive(false);
        // exitBtn = transform.Find("ExitButton").GetComponent<Button>();
        // exitBtn.onClick.AddListener(OnExitClick);
        // exitBtn.gameObject.SetActive(false);

        // quitBattleRequest = GetComponent<QuitBattleRequest>();
    }
    // public override void OnEnter()
    // {
    //     gameObject.SetActive(true);
    // }
    // public override void OnExit()
    // {
    //     successBtn.gameObject.SetActive(false);
    //     failBtn.gameObject.SetActive(false);
    //     exitBtn.gameObject.SetActive(false);
    //     gameObject.SetActive(false);
    // }

    // 控制时间显示
    private void Update()
    {
        if (time > -1)
        {
            ShowTime(time);
            time = -1;
        }
    }
    // private void OnResultClick()
    // {
    //     uiMng.PopPanel();
    //     uiMng.PopPanel();
    //     // facade.GameOver();
    // }
    // private void OnExitClick()
    // {
    //     // quitBattleRequest.SendRequest();
    // }
    // public void OnExitResponse()
    // {
    //     OnResultClick();
    // }

    // 异步显示时间
    public void ShowTimeSync(int time)
    {
        this.time = time;
    }

    public void ShowTime(int time)
    {
        // if (time == 3)
        // {
        //     exitBtn.gameObject.SetActive(true);
        // }
        timer.gameObject.SetActive(true);
        timer.text = time.ToString();
        timer.transform.localScale = Vector3.one; // 大小重置
        Color tempColor = timer.color;
        tempColor.a = 1; // 1显示
        timer.color = tempColor;
        // 从小到大 SetDelay设置延迟
        timer.transform.DOScale(2, 0.3f).SetDelay(0.3f);
        // 进行隐藏
        timer.DOFade(0, 0.3f).SetDelay(0.3f).OnComplete(() => timer.gameObject.SetActive(false));
        facade.PlayNormalSound(AudioManager.Sound_Alert);
    }

    // public void OnGameOverResponse(ReturnCode returnCode)
    // {
    //     Button tempBtn = null;
    //     switch (returnCode)
    //     {
    //         case ReturnCode.Success:
    //             tempBtn = successBtn;
    //             break;
    //         case ReturnCode.Fail:
    //             tempBtn = failBtn;
    //             break;
    //     }
    //     tempBtn.gameObject.SetActive(true);
    //     tempBtn.transform.localScale = Vector3.zero;
    //     tempBtn.transform.DOScale(1, 0.5f);
    // }
}
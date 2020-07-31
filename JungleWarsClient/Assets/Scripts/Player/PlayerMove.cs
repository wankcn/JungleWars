using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // public float forward = 0;

    private float speed = 3;
    private Animator anim; // 控制动画

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 攻击状态下不控制移动
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded") == false) return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 当有键按下的时候再进行移动控制
        if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
        {
            // 控制移动使用世界坐标Space.World
            transform.Translate(new Vector3(h, 0, v) * (speed * Time.deltaTime), Space.World);
            // 朝向设置与按键方向一致
            transform.rotation = Quaternion.LookRotation(new Vector3(h, 0, v));
            float res = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));
            // forward = res;
            anim.SetFloat("Forward", res);
        }
    }
}
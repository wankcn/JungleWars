using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public GameObject arrowPrefab;
    private Animator anim; // 通过Animator得到当前动画状态
    private Transform leftHandTrans; // 箭的位置是左手的位置
    private Vector3 shootDir;
    // private PlayerManager playerMng;
    
    void Start () {
        anim = GetComponent<Animator>();
        leftHandTrans = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");
    }
    
    void Update () {
        // 判断当前状态 是否是在地面
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            // 按下鼠标坐标攻击 攻击到鼠标点下的位置 控制发射方向 按下执行一次
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition); // 将鼠标的点转换成射线
                RaycastHit hit; // 利用射线与环境做碰撞检测是否碰撞到任何东西
                bool isCollider = Physics.Raycast(ray, out hit);
                if (isCollider)
                {
                    Vector3 targetPoint = hit.point;
                    targetPoint.y = transform.position.y; // 方位的变化高度保持不变
                    shootDir = targetPoint - transform.position;  // 得到射箭方向
                    transform.rotation = Quaternion.LookRotation(shootDir);
                    anim.SetTrigger("Attack"); // 播放箭的动画
                    Invoke("Shoot", 0.1f); // 延迟调用，时转向和箭实例化同步
                }
            }
        }
    }
    // public void SetPlayerMng(PlayerManager playerMng)
    // {
    //     this.playerMng = playerMng;
    // }
    
    // 发射方法
    // private void Shoot()
    // {
    //     // playerMng.Shoot(arrowPrefab, leftHandTrans.position, Quaternion.LookRotation(shootDir));
    // }
    
    private void Shoot()
    {
        // 控制箭的位置 
        GameObject.Instantiate(arrowPrefab, leftHandTrans.position, Quaternion.LookRotation(shootDir));
    }
}
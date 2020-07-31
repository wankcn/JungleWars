using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class Arrow : MonoBehaviour
{
    // public RoleType roleType;
    public int speed = 3; // 每秒5米
    // public GameObject explosionEffect;
    // public bool isLocal = false;

    private Rigidbody rgd; //使用刚体控制移动

    // Use this for initialization
    void Start()
    {
        rgd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 移动自身的局部坐标 MovePosition实现平滑的移动
        rgd.MovePosition(transform.position + transform.forward * (speed * Time.deltaTime));
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         GameFacade.Instance.PlayNormalSound(AudioManager.Sound_ShootPerson);
    //         if (isLocal)
    //         {
    //             bool playerIsLocal = other.GetComponent<PlayerInfo>().isLocal;
    //             if (isLocal != playerIsLocal)
    //             {
    //                 GameFacade.Instance.SendAttack(Random.Range(10, 20));
    //             }
    //         }
    //     }
    //     else
    //     {
    //         GameFacade.Instance.PlayNormalSound(AudioManager.Sound_Miss);
    //     }
    //
    //     GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
    //     GameObject.Destroy(this.gameObject);
    // }
}
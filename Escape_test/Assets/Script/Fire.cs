using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float FiringCycle = 2f; //발사 주기
    private float CycleTime = 0f; //현재 사이클 시간

    public GameObject Bullet;
    public Transform FirePos;


    void Update()
    {
        CycleTime += Time.deltaTime; //초당 1씩 증가
        if (CycleTime > FiringCycle) //한 사이클 끝났을 때
        {
            //복제한다. //'Bullet'을 'FirePos.transform.position' 위치에 'FirePos.transform.rotation' 회전값으로.
            GameObject bullet = Instantiate(Bullet, FirePos.position, FirePos.rotation);
            CycleTime = 0f;
            if(transform.rotation.y == 1)
                bullet.name = "leftbullet";
        }
    }
}

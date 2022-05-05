using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarawareMami : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    const float Z_DIST_AT_HIT = 1.0f;
    public bool isDead = false;


    const float DEAD_SPD_Y = 30f;

    void Update()
    {
        if (isDead)
        {
            this.transform.position += Vector3.up * DEAD_SPD_Y * Time.deltaTime;
        }
    }


    public void Dead()
    {
        isDead = true;
    }


    public void Hit()
    {
        //マミーン
        this.transform.position += Vector3.forward * Z_DIST_AT_HIT;
    }



}

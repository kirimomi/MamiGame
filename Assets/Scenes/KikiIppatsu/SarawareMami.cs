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
    public bool IsDead
    {
        get { return m_isDead; }

    }

    bool m_isDead = false;
    const float DEAD_SPD_Y = 30f;

    void Update()
    {
        if (IsDead)
        {
            this.transform.position += Vector3.up * DEAD_SPD_Y * Time.deltaTime;
        }
    }


    public void Dead()
    {
        m_isDead = true;
    }


    public void Hit()
    {
        //マミーン
        KikiGameManager.Instance.MakeSerihu("あ～れ～", this.transform.position + Vector3.down * 2.0f);
        KikiGameManager.Instance.PlayAaree();
        Vibration.Long();
        this.transform.position += Vector3.forward * Z_DIST_AT_HIT;
    }



}

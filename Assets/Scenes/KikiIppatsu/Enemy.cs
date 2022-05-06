using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    float m_speed = 30f;
    const float HIT_DISTANCE = 2.0f;

    const float DEAD_SPD_Y = 80f;

    enum EnemyPhase
    {
        active,
        toritsuki,
        dead,
    }

    EnemyPhase m_enemyPhase = EnemyPhase.active;
    void Update()
    {

        switch (m_enemyPhase)
        {
            case EnemyPhase.active:
                Vector3 mamiPos = KikiGameManager.SarawareMami.gameObject.transform.position;
                if (HIT_DISTANCE < Vector3.Distance(this.transform.position, mamiPos))
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, mamiPos, m_speed * Time.deltaTime);
                }

                //生きているマミに十分近づいたら、マミを手繰り寄せて消える
                if (!KikiGameManager.SarawareMami.IsDead && Vector3.Distance(this.transform.position, mamiPos) < HIT_DISTANCE)
                {
                    KikiGameManager.SarawareMami.Hit();
                    //マミの子にする
                    this.transform.parent = KikiGameManager.SarawareMami.gameObject.transform;
                    Vector3 pos = this.transform.localPosition;
                    pos.z = 0.1f;
                    this.transform.localPosition = pos;
                    Destroy(this.GetComponent<SphereCollider>());//コライダを破壊
                    m_enemyPhase = EnemyPhase.toritsuki;
                }
                break;

            case EnemyPhase.toritsuki:
                //特に何もしない
                break;
            case EnemyPhase.dead:
                //昇天する
                this.transform.position += Vector3.up * DEAD_SPD_Y * Time.deltaTime;
                //一定の座標に達したら消す
                if (20f < this.transform.position.y)
                {
                    Destroy(this.gameObject);
                }
                break;
        }
    }


    public void Dead()
    {
        if (m_enemyPhase != EnemyPhase.dead)
        {
            KikiGameManager.Instance.MakeSerihu("どぅえ", transform.position);
            KikiGameManager.Instance.PlayDue();
            Vibration.Short();
            KikiGameManager.Score++;
            m_enemyPhase = EnemyPhase.dead;
        }
    }

    public void SetSpeed(float speed)
    {
        m_speed = speed;
    }
}

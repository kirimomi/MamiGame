using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    float m_speed = 30f;
    const float HIT_DISTANCE = 2.0f;

    bool isActive = true;
    void Update()
    {

        if (isActive)
        {
            Vector3 mamiPos = KikiGameManager.SarawareMami.gameObject.transform.position;
            if (HIT_DISTANCE < Vector3.Distance(this.transform.position, mamiPos))
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, mamiPos, m_speed * Time.deltaTime);
            }

            //生きているマミに十分近づいたら、マミを手繰り寄せて消える
            if (isActive &&
                !KikiGameManager.SarawareMami.isDead &&
                Vector3.Distance(this.transform.position, mamiPos) < HIT_DISTANCE)
            {
                KikiGameManager.SarawareMami.Hit();
                isActive = false;
                this.transform.parent = KikiGameManager.SarawareMami.gameObject.transform;
                Vector3 pos = this.transform.localPosition;
                pos.z = 0.1f;
                this.transform.localPosition = pos;
                Destroy(this.GetComponent<SphereCollider>());//コライダを破壊
                //Destroy(this.gameObject);
            }
        }
    }


    public void SetSpeed(float speed)
    {
        m_speed = speed;
    }
}

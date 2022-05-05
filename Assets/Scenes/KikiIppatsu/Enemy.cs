using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    [SerializeField] float SPEED = 100f;

    const float HIT_DISTANCE = 2.0f;

    bool isActive = true;
    void Update()
    {

        if (isActive)
        {
            Vector3 mamiPos = GameManager.SarawareMami.gameObject.transform.position;
            if (HIT_DISTANCE < Vector3.Distance(this.transform.position, mamiPos))
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, mamiPos, SPEED * Time.deltaTime);
            }

            //生きているマミに十分近づいたら、マミを手繰り寄せて消える
            if (isActive &&
                !GameManager.SarawareMami.isDead &&
                Vector3.Distance(this.transform.position, mamiPos) < HIT_DISTANCE)
            {
                GameManager.SarawareMami.Hit();
                isActive = false;
                this.transform.parent = GameManager.SarawareMami.gameObject.transform;
                Vector3 pos = this.transform.localPosition;
                pos.z = 0.1f;
                this.transform.localPosition = pos;
                Destroy(this.GetComponent<SphereCollider>());//コライダを破壊
                //Destroy(this.gameObject);
            }
        }
    }
}

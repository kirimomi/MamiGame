using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject m_mami;

    [SerializeField] Slider m_mamiPosSlider;

    static public SarawareMami SarawareMami;

    float mamiStartPosZ;
    const float MAMI_END_POS_Z = 20f;


    void Start()
    {
        SarawareMami = m_mami.GetComponent<SarawareMami>();
        mamiStartPosZ = m_mami.transform.position.z;
    }


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Enemy")
                {
                    Debug.Log("RayがEnemyに当たった");
                    Destroy(hit.collider.gameObject);
                }
            }
        }


        //マミ位置スライダー
        float current = m_mami.transform.position.z - mamiStartPosZ;
        m_mamiPosSlider.value = current / (MAMI_END_POS_Z - mamiStartPosZ);

        //連れ去られた
        if (MAMI_END_POS_Z < m_mami.transform.position.z)
        {
            SarawareMami.Dead();//死亡
        }

    }
}
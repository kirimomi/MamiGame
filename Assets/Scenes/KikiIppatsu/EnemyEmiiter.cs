using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmiiter : MonoBehaviour
{

    public GameObject EnemyPrefab;


    float m_intervalCount = 0;

    const float INTERVAL_SEC = 0.2f;
    const float ENEMY_RANGE_X = 25f;
    const float ENEMY_RANGE_Y = 15f;

    struct phase
    {
        public float count;
        public float interval;
        public float minSpeed;
        public float maxSpeed;
    };

    int m_phaseNow = 0;
    phase[] m_phase = new phase[5];

    float m_phaseCount = 0;

    void Start()
    {
        m_phase[0].count = 5f;
        m_phase[0].interval = 0.4f;
        m_phase[0].minSpeed = 5f;
        m_phase[0].maxSpeed = 25f;

        m_phase[1].count = 5f;
        m_phase[1].interval = 0.3f;
        m_phase[1].minSpeed = 5f;
        m_phase[1].maxSpeed = 25f;

        m_phase[2].count = 5f;
        m_phase[2].interval = 0.2f;
        m_phase[2].minSpeed = 5f;
        m_phase[2].maxSpeed = 25f;

        m_phase[3].count = 5f;
        m_phase[3].interval = 0.1f;
        m_phase[3].minSpeed = 10f;
        m_phase[3].maxSpeed = 25f;

        m_phase[4].count = 5f;
        m_phase[4].interval = 0.05f;
        m_phase[4].minSpeed = 15f;
        m_phase[4].maxSpeed = 30f;
    }

    void Update()
    {

        //フェーズ切り替え
        if (m_phase[m_phaseNow].count < m_phaseCount)
        {
            //最後のフェーズの場合は固定する
            if (m_phaseNow < m_phase.Length - 1)
            {
                m_phaseNow++;
                m_phaseCount = 0;
            }
        }
        Debug.Log("phase is " + m_phaseNow.ToString());
        m_phaseCount += Time.deltaTime;

        if (!KikiGameManager.Instance.IsGameEnd && m_phase[m_phaseNow].interval < m_intervalCount)
        {
            Vector3 pos = this.transform.position;
            pos += Vector3.right * ENEMY_RANGE_X * Random.Range(-1f, 1f);
            pos += Vector3.up * ENEMY_RANGE_Y * Random.Range(-1f, 1f);
            GameObject obj = Instantiate(EnemyPrefab, pos, Quaternion.identity);
            float minSpd = m_phase[m_phaseNow].minSpeed;
            float maxSpd = m_phase[m_phaseNow].maxSpeed;
            obj.GetComponent<Enemy>().SetSpeed(minSpd + Random.Range(0, maxSpd - minSpd));
            m_intervalCount = 0;
        }
        m_intervalCount += Time.deltaTime;

    }

}
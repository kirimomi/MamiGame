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

    const float ENEMY_SPD_MIN = 5;
    const float ENEMY_SPD_MAX = 25;


    struct phase
    {
        public float count;
        public float interval;
    };

    int m_phaseNow = 0;
    phase[] m_phase = new phase[4];

    float m_phaseCount = 0;

    void Start()
    {
        m_phase[0].count = 5f;
        m_phase[0].interval = 0.4f;

        m_phase[1].count = 5f;
        m_phase[1].interval = 0.3f;

        m_phase[2].count = 5f;
        m_phase[2].interval = 0.2f;

        m_phase[3].count = 5f;
        m_phase[3].interval = 0.1f;

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
            obj.GetComponent<Enemy>().SetSpeed(ENEMY_SPD_MIN + Random.Range(0, ENEMY_SPD_MAX - ENEMY_SPD_MIN));
            m_intervalCount = 0;
        }
        m_intervalCount += Time.deltaTime;

    }

}
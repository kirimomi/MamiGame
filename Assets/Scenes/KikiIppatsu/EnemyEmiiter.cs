using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmiiter : MonoBehaviour
{

    public GameObject EnemyPrefab;


    // Use this for initialization
    void Start()
    {
    }


    float m_counter = 0;

    const float INTERVAL_SEC = 0.2f;

    const float ENEMY_RANGE_X = 25f;
    const float ENEMY_RANGE_Y = 15f;



    void Update()
    {

        if (INTERVAL_SEC < m_counter)
        {
            Vector3 pos = this.transform.position;
            pos += Vector3.right * ENEMY_RANGE_X * Random.Range(-1f, 1f);
            pos += Vector3.up * ENEMY_RANGE_Y * Random.Range(-1f, 1f);
            GameObject obj = Instantiate(EnemyPrefab, pos, Quaternion.identity);
            m_counter = 0;
        }
        m_counter += Time.deltaTime;
    }

}
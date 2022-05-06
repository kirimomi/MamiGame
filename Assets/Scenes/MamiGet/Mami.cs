using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class Mami : MonoBehaviour
{

    Vector3 m_speed = Vector3.zero;

    public GameObject SerihuPrefab;
    public AudioClip GetSe;
    public AudioClip DropSe;

    void Start()
    {
        if (m_speed == Vector3.zero)
        {
            m_speed = new Vector3(2f, -4f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (0 < MainSystem.Counter)
        {
            transform.position += m_speed * Time.deltaTime;
            if (transform.position.y < -4.4f)
            {
                MainSystem.Audio.PlayOneShot(DropSe);
                MakeSerihu("あーれー");
                Destroy(gameObject);
            }
        }
    }


    const float SCREEN_EDGE_X = 2.8f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (MainSystem.Counter <= 0)
        {
            return;
        }
        else if (col.tag == "Player")
        {
            MainSystem.Audio.PlayOneShot(GetSe);
            MakeSerihu("マミーン");
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            MainSystem.Score++;
            Destroy(gameObject);
        }
        else if (col.tag == "Wall")
        {
            m_speed.x = -m_speed.x;
            /*
			if (0 < transform.position.x) {
				transform.position = new Vector3 (transform.position.x - SCREEN_EDGE_X * 2f, transform.position.y, transform.position.z);
			} else {
				transform.position = new Vector3 (transform.position.x + SCREEN_EDGE_X * 2f, transform.position.y, transform.position.z);
			}*/
        }
    }

    public void SetSpeed(Vector3 spd)
    {
        m_speed = spd;
        m_speed.z = 0;
    }

    void MakeSerihu(string txt)
    {
        GameObject obj = Instantiate(SerihuPrefab, transform.position, Quaternion.identity);
        obj.transform.Find("TextMesh").GetComponent<TextMesh>().text = txt;
    }

}

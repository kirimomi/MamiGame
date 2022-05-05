using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KikiGameManager : MonoBehaviour
{

    [SerializeField] GameObject m_mami;

    [SerializeField] Slider m_mamiPosSlider;

    [SerializeField] GameObject m_serihuPrefab;


    [SerializeField] AudioClip m_bgm;
    [SerializeField] AudioClip m_end;

    [SerializeField] AudioClip m_due;
    [SerializeField] AudioClip m_aaree;

    [SerializeField] GameObject m_buttons;



    public static SarawareMami SarawareMami;

    public static AudioSource Audio;

    public static KikiGameManager Instance;


    bool m_isGameEnd = false;

    public bool IsGameEnd
    {
        get { return m_isGameEnd; }
    }

    float mamiStartPosZ;
    const float MAMI_END_POS_Z = 20f;


    void Start()
    {
        SarawareMami = m_mami.GetComponent<SarawareMami>();
        mamiStartPosZ = m_mami.transform.position.z;


        Audio = GetComponent<AudioSource>();
        Audio.clip = m_bgm;
        Audio.Play();
        //Buttons.SetActive(false);
        //BackGround.SetActive(false);

        Instance = this;
        m_buttons.SetActive(false);

    }


    void Update()
    {
        if (!m_isGameEnd)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        MakeSerihu("どぅえ", hit.collider.gameObject.transform.position);
                        Audio.PlayOneShot(m_due);
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

            //マミがさらわれた
            if (20f < m_mami.transform.position.y)
            {
                m_isGameEnd = true;
                Audio.Stop();
                Audio.PlayOneShot(m_end);
                m_buttons.SetActive(true);
            }
        }
    }

    public void MakeSerihu(string txt, Vector3 pos)
    {
        GameObject obj = Instantiate(m_serihuPrefab, pos, Quaternion.identity);
        obj.transform.Find("TextMesh").GetComponent<TextMesh>().text = txt;
    }

    public void PlayAaree()
    {
        Audio.PlayOneShot(m_aaree);
    }

    public void OnButtonReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnButtonReturn()
    {
        SceneManager.LoadScene("Selector");
    }
}
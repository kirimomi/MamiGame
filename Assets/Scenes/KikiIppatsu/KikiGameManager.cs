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
    [SerializeField] AudioClip m_nyorimaan;

    [SerializeField] GameObject m_buttons;

    [SerializeField] Text m_scoreText;
    [SerializeField] Text m_highScoreText;



    public static SarawareMami SarawareMami;

    public static AudioSource Audio;

    public static KikiGameManager Instance;

    public static int Score = 0;
    int m_highScore = 0;


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

        Score = 0;
        m_highScore = LoadHighScore();
        if (m_highScore < 0)
        {
            m_highScoreText.text = "";
        }
        else
        {
            m_highScoreText.text = "High : " + m_highScore.ToString();
        }

        Color col = Color.white;
        col.a = 0.3f;
        m_scoreText.color = col;
        m_highScoreText.color = col;

    }


    void Update()
    {
        if (!m_isGameEnd)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //if (Input.GetMouseButtonDown(0))
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        hit.collider.gameObject.GetComponent<Enemy>().Dead();
                    }
                }
            }



            //連れ去られた
            if (!SarawareMami.IsDead && MAMI_END_POS_Z < m_mami.transform.position.z)
            {
                SarawareMami.Dead();//死亡
                Audio.Stop();
                Audio.PlayOneShot(m_nyorimaan);
            }

            //マミが画面から消え、ゲーム終了
            if (20f < m_mami.transform.position.y)
            {
                m_isGameEnd = true;

                //Audio.PlayOneShot(m_end);
                m_buttons.SetActive(true);

                m_scoreText.color = Color.white;
                m_highScoreText.color = Color.white;

                if (m_highScore < Score)
                {
                    SaveHighScore(Score);
                }
            }


            //UI
            //マミ位置スライダー
            float current = m_mami.transform.position.z - mamiStartPosZ;
            m_mamiPosSlider.value = current / (MAMI_END_POS_Z - mamiStartPosZ);

            //Score
            m_scoreText.text = Score.ToString();

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

    public void PlayDue()
    {
        Audio.PlayOneShot(m_due);
    }


    public void OnButtonReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnButtonReturn()
    {
        SceneManager.LoadScene("Selector");
    }



    const string HIGH_SCORE_KEY = "highScoreKiki";

    void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
        PlayerPrefs.Save();
    }

    int LoadHighScore()
    {
        //Debug.Log ("high score is " + PlayerPrefs.GetInt (HIGH_SCORE_KEY, -1).ToString ());
        return PlayerPrefs.GetInt(HIGH_SCORE_KEY, -1);
    }



}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{


    public static AudioSource Audio;
    public AudioClip SE;

    public Image[] ButtonImage;


    // Use this for initialization
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        Audio.clip = SE;
        StartCoroutine(Main());
    }


    Color Str2Color(string str)
    {
        Color color = default(Color);
        if (ColorUtility.TryParseHtmlString(str, out color))
        {
            // 成功
            return color;
        }
        else
        {
            return Color.white;
        }
    }


    IEnumerator Main()
    {
        while (true)
        {
            if (0 < m_pressedButton)
            {
                Audio.Play();

                Image target;

                target = ButtonImage[m_pressedButton - 1];

                //点滅
                target.color = Str2Color("#FF9C9CFF");
                yield return new WaitForSeconds(0.2f);
                target.color = Str2Color("#FFFFFFFF");
                yield return new WaitForSeconds(0.2f);

                target.color = Str2Color("#FF9C9CFF");
                yield return new WaitForSeconds(0.2f);
                target.color = Str2Color("#FFFFFFFF");
                yield return new WaitForSeconds(0.2f);

                target.color = Str2Color("#FF9C9CFF");
                yield return new WaitForSeconds(0.2f);
                target.color = Str2Color("#FFFFFFFF");
                yield return new WaitForSeconds(0.2f);

                target.color = Str2Color("#FF9C9CFF");
                yield return new WaitForSeconds(0.2f);
                target.color = Str2Color("#FFFFFFFF");
                yield return new WaitForSeconds(0.2f);

                target.color = Str2Color("#FF9C9CFF");
                yield return new WaitForSeconds(0.2f);
                target.color = Str2Color("#FFFFFFFF");
                yield return new WaitForSeconds(0.2f);


                SceneManager.LoadScene(m_sceneName);
                yield break;
            }
            yield return null;
        }
    }


    int m_pressedButton = -1;
    string m_sceneName = "";

    public void OnButtonMamiGet()
    {
        m_pressedButton = 1;
        m_sceneName = "MamiGet";
    }

    public void OnButtonIkariMami()
    {
        m_pressedButton = 2;
        m_sceneName = "IkariMami";
    }

    public void OnButtonKikiIppatsu()
    {
        m_pressedButton = 3;
        m_sceneName = "KikiIppatsu";
    }
}

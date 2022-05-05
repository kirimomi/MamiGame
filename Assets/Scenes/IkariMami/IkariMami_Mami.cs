using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkariMami_Mami : MonoBehaviour
{

    public Sprite SpriteNormal;
    public Sprite SpriteWarai;
    public Sprite SpriteIkari;
    public Sprite SpriteNorio;

    public SpriteRenderer MamiFace;

    public GameObject SerihuPrefab;
    public AudioClip SeGood;
    public AudioClip SeBad;
    public AudioClip SeNorio;

    public enum MamiMode
    {
        Warai,
        Normal,
        Ikari,
        Norio,
    }

    MamiMode m_mode = MamiMode.Normal;

    public MamiMode Mode { get { return m_mode; } set { m_mode = value; } }


    // Use this for initialization
    void Start()
    {
        /*
		if (Random.value < 0.1f) {
			m_mode = MamiMode.Ikari;
		} else if(Random.value < 0.2f){
			m_mode = MamiMode.Warai;
		}else if(Random.value < 0.4f){
			m_mode = MamiMode.Norio;
		}*/

        SetFaceSprite();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Touched()
    {

        if (m_mode == MamiMode.Norio)
        {
            IkariMamiMainSystem.Audio.PlayOneShot(SeNorio);
            MakeSerihu("どぅぇ");
            Destroy(gameObject);
        }


        switch (m_mode)
        {
            case MamiMode.Warai:
                m_mode = MamiMode.Ikari;
                IkariMamiMainSystem.Audio.PlayOneShot(SeBad);
                MakeSerihu("あーれー");
                break;
            case MamiMode.Normal:
                m_mode = MamiMode.Warai;
                IkariMamiMainSystem.Audio.PlayOneShot(SeGood);
                MakeSerihu("マミーン");
                break;
            case MamiMode.Ikari:
                m_mode = MamiMode.Normal;
                IkariMamiMainSystem.Audio.PlayOneShot(SeGood);
                MakeSerihu("マミーン");
                break;
        }
        SetFaceSprite();
    }

    void SetFaceSprite()
    {
        //Debug.Log ("hoge");
        switch (m_mode)
        {
            case MamiMode.Warai:
                MamiFace.sprite = SpriteWarai;
                break;
            case MamiMode.Normal:
                MamiFace.sprite = SpriteNormal;
                break;
            case MamiMode.Ikari:
                MamiFace.sprite = SpriteIkari;
                break;
            case MamiMode.Norio:
                MamiFace.sprite = SpriteNorio;
                break;
        }
    }

    void MakeSerihu(string txt)
    {
        GameObject obj = Instantiate(SerihuPrefab, transform.position, Quaternion.identity);
        obj.transform.Find("TextMesh").GetComponent<TextMesh>().text = txt;
    }

}

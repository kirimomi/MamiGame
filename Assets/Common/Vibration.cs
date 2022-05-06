using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void Long()
    {

#if UNITY_IOS
        UniIosAudioService.PlaySystemSound(1520);
#endif

#if UNITY_ANDROID
        UniAndroidVibration.Vibrate(20);
#endif

    }

    public static void Short()
    {
#if UNITY_IOS
        UniIosAudioService.PlaySystemSound(1521);
#endif

#if UNITY_ANDROID
        UniAndroidVibration.Vibrate(100);
#endif

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_STANDALONE
        gameObject.SetActive(false);
#elif UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
        gameObject.SetActive(true);
#endif
    }
}

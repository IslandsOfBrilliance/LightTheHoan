#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
#if UNITY_POST_PROCESSING_STACK_V1 && !UNITY_POST_PROCESSING_STACK_V2 && AQUAS_PRESENT
using UnityEngine.PostProcessing;
#endif


public class AQUAS_CameraSwitcher : MonoBehaviour {

    public GameObject camerappv1;
    public GameObject camerappv2;

	// Use this for initialization
	void Awake () {

#if AQUAS_PRESENT && UNITY_POST_PROCESSING_STACK_V1
        camerappv2.SetActive(false);
        camerappv1.SetActive(true);
#endif


    }
}

#endif

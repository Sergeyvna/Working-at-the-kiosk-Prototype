using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaToEnter : MonoBehaviour
{
    [SerializeField]
    private int sceneIndx;
    
    private BackgroundAudio backgroundAudio;

    // Start is called before the first frame update
    void Start()
    {
        if(sceneIndx == Player.instance.sceneIndx)
        {
            Player.instance.transform.position = transform.position;
        }
    }
}

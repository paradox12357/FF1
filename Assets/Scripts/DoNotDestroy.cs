using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    public static DoNotDestroy instance;
    private void Awake()
    {
        // these 6 lines of code makes it so this object doesn't destroyed when loading onto another scene
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if(musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

}

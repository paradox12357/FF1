using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMusic : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "CarSelect")
        {
            DoNotDestroy.instance.GetComponent<AudioSource>().Stop();
        }
    }
}

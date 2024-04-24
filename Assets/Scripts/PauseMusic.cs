using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMusic : MonoBehaviour
{
    public static PauseMusic instance;
    public AudioSource music;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is "CarSelect"
        if (scene.name == "CarSelect" || scene.name == "DoubleStar")
        {
            music.Pause();
        }
        else
        {
            if (!music.isPlaying)
            {
                music.Play();
            }
        }
    }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        music = GetComponent<AudioSource>();
    }

    void OnDestroy()
    {
        // Always good practice to remove listeners when no longer needed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}

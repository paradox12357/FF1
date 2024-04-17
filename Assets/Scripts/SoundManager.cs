using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{   
    // use SerializeField when we want variable to be PRIVATE BUT ALSO SEEN IN EDITOR
    [SerializeField] Slider volumeSlider;

    public GameObject ObjectMusic;

    private AudioSource AudioSource;
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolumn", 1);
            musicLoad();
        }
        else
        {
            musicLoad();
        }

        ObjectMusic = GameObject.FindWithTag("GameMusic");
        AudioSource = ObjectMusic.GetComponent<AudioSource>();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        musicSave();
    }

    private void musicLoad()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void musicSave()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionToStart : MonoBehaviour
{
    public void LoadStart()
    {
        SceneManager.LoadScene("Start");
    }
}

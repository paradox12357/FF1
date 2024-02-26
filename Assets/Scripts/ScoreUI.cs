using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public ShipDrive ship;
    private int shipScore;

    // Update is called once per frame
    void Update()
    {
        // set shipScore to the component in ship that tracks score (assuming each ship keeps track of its own score)
    }
}

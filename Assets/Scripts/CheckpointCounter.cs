using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class CheckpointCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        checkpointNames = new String[maxCheckpoints];
        checkpointText.text = "Gates: 0/" + maxCheckpoints;
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static int winners = 0;
    public int checkpointCount = 0;
    public bool hasFinished = false;
    public int maxCheckpoints = 3;
    public String[] checkpointNames;
    public TextMeshProUGUI checkpointText;
    public TextMeshProUGUI Victory;
    public Image image;
    public TextMeshProUGUI score;
    public TextMeshProUGUI speed;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Checkpoint" && checkpointCount < maxCheckpoints && !checkpointNames.Contains(other.gameObject.name))
        {
            checkpointNames[checkpointCount] = other.gameObject.name;
            checkpointCount++;
            Debug.Log("Checkpoint Count: " + checkpointCount);
            checkpointText.text = "Gates: " + checkpointCount + "/" + maxCheckpoints;
        }
        if (other.gameObject.tag == "Finish" && checkpointCount >= maxCheckpoints)
        {
            hasFinished = true;
            Debug.Log("You have reached the finish line!");
            checkpointText.text = "GOAL!";
            winners++;
            image.enabled = true;
            checkpointText.enabled = false;
            score.enabled = false;
            speed.enabled = false;
            switch (winners)
            {
                case 1:
                    Victory.text = "1st Place!";
                    image.color = new Color(212/255f,175/255f,55/255f);
                    //image.color = new Color(0, 10, 55);
                    break;
                case 2:
                    Victory.text = "2nd Place!";
                    image.color = new Color(192/255f, 192 / 255f, 192 / 255f);
                    break;
                case 3:
                    Victory.text = "3rd Place!";
                    image.color = new Color(205 / 255f, 127 / 255f, 50 / 255f);
                    break;
                default:
                    Victory.text = winners + "th Place!";
                    image.color = new Color(0f, 0f, 0f);
                    break;
            }

        }
    }
}


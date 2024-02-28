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
        checkpointText.text = "CP: 0/" + maxCheckpoints;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int checkpointCount = 0;
    public bool hasFinished = false;
    public int maxCheckpoints = 3;
    public String[] checkpointNames;
    public TextMeshProUGUI checkpointText;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Checkpoint" && checkpointCount < maxCheckpoints && !checkpointNames.Contains(other.gameObject.name))
        {
            checkpointNames[checkpointCount] = other.gameObject.name;
            checkpointCount++;
            Debug.Log("Checkpoint Count: " + checkpointCount);
            checkpointText.text = "CP: " + checkpointCount + "/" + maxCheckpoints;
        }
        if (other.gameObject.tag == "Finish" && checkpointCount >= maxCheckpoints)
        {
            hasFinished = true;
            Debug.Log("You have reached the finish line!");
            checkpointText.text = "GOAL!";
        }
    }
}


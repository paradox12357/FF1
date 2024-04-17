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
        checkpointNames = new String[maxScore];
        checkpointText.text = "Score: 0/" + maxScore;
        image.enabled = false;
        Players++;
        Player = Players;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static int winners = 0;
    public int scoreCount = 0;
    public bool hasFinished = false;
    public int maxScore = 10000;
    public String[] checkpointNames;
    public TextMeshProUGUI checkpointText;
    public TextMeshProUGUI Victory;
    public Image image;
    public TextMeshProUGUI score;
    public TextMeshProUGUI speed;
    public static int Players = 0;
    public int Player = 0;
    public GameObject playermesh;
    [SerializeField] public Camera cam;
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Checkpoint" && scoreCount <= maxScore && !checkpointNames.Contains(other.gameObject.name))
        {
            checkpointNames[scoreCount] = other.gameObject.name;
            scoreCount += 1000;
            FindObjectOfType<SoundEffectPlayer>().Play("checkpointHit");//Possibly have this pitch upward every time you hit another checkpoint?
            Debug.Log("Score: " + scoreCount);
            checkpointText.text = "Score: " + scoreCount + "/" + maxScore;
            switch (Player)
            {
                case 1:
                    other.gameObject.GetComponent<hider>().hide(1);
                    break;
                case 2:
                    other.gameObject.GetComponent<hider>().hide(2);
                    break;
                case 3:
                    other.gameObject.GetComponent<hider>().hide(3);
                    break;
                default:
                    other.gameObject.GetComponent<hider>().hide(4);
                    break;
            }
        }
        if (other.gameObject.tag == "Finish" && scoreCount >= maxScore)
        {
            hasFinished = true;
            Debug.Log("You have reached the finish line!");
            checkpointText.text = "GOAL!";
            winners++;
            image.enabled = true;
            checkpointText.enabled = false;
            score.enabled = false;
            speed.enabled = false;
            playermesh.SetActive(false);
            cam.cullingMask &= (1 << 5);
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
    public void updateCheckpoint()
    {
        checkpointText.text = "Score: " + scoreCount + "/" + maxScore;
    }
}


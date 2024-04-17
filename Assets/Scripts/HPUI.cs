using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPUI : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public ShipDrive ship;


    // Update is called once per frame
    void Update()
    {
        hpText.text = "HP: " + ship.HP + "/100";
    }
}

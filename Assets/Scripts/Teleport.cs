using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Player;
    public GameObject TeleportA;
    public GameObject TeleportB;
    public GameObject TeleportC;
    public GameObject TeleportD;
    public GameObject TeleportE;
    public GameObject TeleportF;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("TeleportA"))
        {
            Player.transform.position = TeleportA.transform.position;
        }

        if (collision.gameObject.CompareTag("TeleportB"))
        {
            Player.transform.position = TeleportB.transform.position;
        }

        if (collision.gameObject.CompareTag("TeleportC"))
        {
            Player.transform.position = TeleportC.transform.position;
        }

        if (collision.gameObject.CompareTag("TeleportD"))
        {
            Player.transform.position = TeleportD.transform.position;
        }

        if (collision.gameObject.CompareTag("TeleportE"))
        {
            Player.transform.position = TeleportE.transform.position;
        }

        if (collision.gameObject.CompareTag("TeleportF"))
        {
            Player.transform.position = TeleportF.transform.position;
        }
    }
}

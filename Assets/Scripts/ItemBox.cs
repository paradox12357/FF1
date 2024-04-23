using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{

    private string[] itemList = { "Boost Rocket", "Gun", "Rocket", "Grapple Hook", "Speedboost", "Phantom", "Obliterator" };
    private int randomItem;
    private BoxCollider boxCollider;
    private MeshRenderer mesh;
    public Sprite rocketSprite;
    public Sprite hRocketSprite;
    public Sprite boostSprite;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Rotate(0.0f, 1.0f, 0.0f, Space.Self);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<ShipDrive>().currentItem.Equals("None"))
            {
                randomItem = UnityEngine.Random.Range(0, 100);
                FindObjectOfType<SoundEffectPlayer>().Play("itemGet");
                if (randomItem >= 0 && randomItem < 34)
                {
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = itemList[0];
                    collision.gameObject.GetComponent<ShipDrive>().itemImage.sprite = hRocketSprite;
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = "rocketHoming";
                }
                else if (randomItem >= 34 && randomItem < 67)
                {
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = itemList[2];
                    collision.gameObject.GetComponent<ShipDrive>().itemImage.sprite = rocketSprite;
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = "rocket";
                }
                else if (randomItem >= 67 && randomItem < 100)
                {
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = itemList[4];
                    collision.gameObject.GetComponent<ShipDrive>().itemImage.sprite = boostSprite;
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = "boost";
                }
                /*
                if (randomItem >= 0 && randomItem < 16)
                {
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = itemList[0];
                }
                else if (randomItem >= 16 && randomItem < 32)
                {
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = itemList[1];
                }
                else if (randomItem >= 32 && randomItem < 48)
                {
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = itemList[2];
                }
                else if (randomItem >= 48 && randomItem < 64)
                {
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = itemList[3];
                }
                else if (randomItem >= 64 && randomItem < 80)
                {
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = itemList[4];
                }
                else if (randomItem >= 80 && randomItem < 96)
                {
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = itemList[5];
                }
                else
                {
                    collision.gameObject.GetComponent<ShipDrive>().currentItem = itemList[6];
                }
                */

                //boxCollider.enabled = !boxCollider.enabled;
                //mesh.enabled = !mesh.enabled;
            }
        }
    }
}

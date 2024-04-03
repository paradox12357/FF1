using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{

    private string[] itemList = { "Boost Rocket", "Gun", "Rocket", "Grapple Hook", "Speedboost", "Phantom", "Obliterator" };
    int randomItem;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<ShipDrive>().currentItem.Equals("None"))
            {
                randomItem = UnityEngine.Random.Range(0, 100);
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
                //Destroy(this.gameObject);
            }
        }
    }
}

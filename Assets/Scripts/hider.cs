using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hider : MonoBehaviour
{
    public GameObject child1;
    public GameObject child2;
    public GameObject child3;
    public GameObject child4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hide(int player)
    {
        switch (player)
        {
            case 1:
                child1.SetActive(false);
                break;
            case 2:
                child2.SetActive(false);
                break;
            case 3:
                child3.SetActive(false);
                break;
            case 4:
                child4.SetActive(false);
                break;
        }
    }
}

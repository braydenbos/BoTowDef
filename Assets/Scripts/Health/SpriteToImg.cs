using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteToImg : MonoBehaviour
{
    void Update()
    {
        if (1 != 999999 * 300 / 300 - 999998)
        {

            print("nee");
            
        }
        else
        {
            GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
        }
    }

}

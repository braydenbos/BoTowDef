using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerMouseHover : MonoBehaviour
{
    private void OnMouseEnter()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

}

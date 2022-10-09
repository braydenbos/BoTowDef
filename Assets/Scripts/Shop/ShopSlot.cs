using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public GameObject item;
    public int cost;
    private void Start()
    {
        item.transform.GetChild(0).GetComponent<dragAndDrop>().cost = cost;
    }
    private void Update()
    {
        if (item.transform.GetChild(0).GetComponent<dragAndDrop>().isGrabed)
        {
            GameObject newItem = Instantiate(item, transform);
            newItem.transform.localPosition = new Vector3(0, 0.1f, 0);
            item.transform.GetChild(0).gameObject.AddComponent<towerMouseHover>();
            item.GetComponent<SpriteRenderer>().sortingOrder = 2;
            item.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 3;

            item = newItem;
            item.transform.GetChild(0).GetComponent<dragAndDrop>().cost = cost;
        }
    }
}

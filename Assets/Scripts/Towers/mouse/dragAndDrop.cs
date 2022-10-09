using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragAndDrop : MonoBehaviour
{
    private bool isCollided;
    private Towers towers;
    public bool isGrabed;
    private Transform towerparent;
    private Transform parent;
    private SpriteRenderer PSR;
    private SpriteRenderer SR;
    private Shop shop;
    public int cost;


    private void Start()
    {
        shop = GameObject.Find("Shop").GetComponent<Shop>();
        isCollided = false;
        isGrabed = false;
        parent = transform.parent;
        PSR = parent.GetComponent<SpriteRenderer>();
        SR = GetComponent<SpriteRenderer>();
        towerparent = GameObject.Find("towers").transform;
        towers = transform.parent.GetComponent<Towers>();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).localScale = new Vector2(towers.range*4, towers.range*4);
    }
    private void OnMouseDown() 
    {
        transform.GetChild(0).gameObject.SetActive(true);
        isGrabed = true;
        parent.SetParent(towerparent);
        parent.localScale = new Vector3(1, 1, 1);
    }
    private void OnMouseUp()
    {
        isGrabed = false;
        if (isCollided || shop.gold < cost)
        {
            Destroy(parent.gameObject);
        }
        else
        {
            shop.updateGold(-cost);
            Destroy(GetComponent<dragAndDrop>());
            parent.GetComponent<Towers>().TowerUpdate();
            GetComponent<CircleCollider2D>().isTrigger = false;
            gameObject.layer++;
        }
    }
    private void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.parent.Translate(mousePosition);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isGrabed)
        {
            PSR.color = Color.red;
            transform.GetChild(0).gameObject.SetActive(false);
            if (PSR.GetComponent<SpriteRenderer>().sortingOrder < collision.GetComponent<SpriteRenderer>().sortingOrder)
            {
                PSR.sortingOrder = collision.GetComponent<SpriteRenderer>().sortingOrder + 1;
                SR.sortingOrder = PSR.sortingOrder + 1;
            }
        }
        isCollided = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isGrabed)
        {
            PSR.color = Color.white;
            transform.GetChild(0).gameObject.SetActive(true);
            PSR.sortingOrder = 2;
            GetComponent<SpriteRenderer>().sortingOrder = PSR.sortingOrder + 1;
        }
        isCollided = false;
    }
}
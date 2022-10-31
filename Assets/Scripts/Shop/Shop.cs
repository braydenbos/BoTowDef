using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public float movementTimer;
    public float timer;
    public bool opened;
    public TextMeshProUGUI coins;
    public int gold;
    // Update is called once per frame
    private void Start()
    {
        opened = false;
    }
    void Update()
    {
        float scale = Camera.main.orthographicSize;
        transform.localScale = new Vector3(scale * (3.55f+0.01f/3), scale/3*2,1);
        transform.localPosition = new Vector3(0,(-scale/10*9)-(movementTimer-timer)/movementTimer*scale, transform.localPosition.z);
        if (opened && timer < movementTimer)
        {
            timer += Time.deltaTime;
        }
        else if(timer > movementTimer)
        {
            timer=movementTimer;
        }
        if(!opened && timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(timer < 0)
        {
            timer = 0;
        }
    }
    public void updateGold(int change)
    {
        gold += change;
        coins.text = "Gold: " + gold;
    }
    public void ShopActivate()
    {
        opened = !opened;
    }
}

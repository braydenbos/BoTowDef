using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyMove : MonoBehaviour
{
    readonly List<Transform> cords = new();
    public GameObject hp;
    private Transform cordP;
    private int at;
    public float movementSpeed;
    public int maxhealthpoints;
    private int healthpoints;
    public int path;
    public int earn;
    private Shop shop;
    private Vector3 curpos;
    private Vector3 lastpos;
    public Vector3 UDir => (curpos - lastpos).normalized;
    private void Start()
    {
        healthpoints = maxhealthpoints;
        shop = GameObject.Find("Shop").GetComponent<Shop>();
        curpos = transform.position;
    }
    private void Update()
    {
        lastpos = curpos;
        curpos = transform.position;
        if (transform.position != cords[at].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, cords[at].position, movementSpeed * Time.deltaTime);
        }
        else if(cords.Count > at + 1)
        {
            at++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void FindPath()
    {
        cordP = GameObject.Find("wpoints" + path).transform;
        for (int i = 0; i < cordP.childCount; i++)
        {
            cords.Add(cordP.GetChild(i).transform);
        }
    }
    public void Damage(int hit)
    {
        if (hit > 0)
        {
            healthpoints -= hit;
            GameObject amount = Instantiate(hp, GameObject.Find("Canvas").transform);
            amount.transform.position = Camera.main.WorldToScreenPoint(new Vector2(transform.position.x + 2 + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f)));
            amount.GetComponent<TextMeshProUGUI>().text = "-" + hit + " hp";
            Destroy(amount, 1);
            Transform bar = transform.GetChild(0).GetChild(0);
            bar.localScale = new Vector3(0.97f*healthpoints/maxhealthpoints, 0.88f,0);
            bar.localPosition = new Vector3(0.97f * bar.localScale.x/2 - 0.97f / 2, 0, 0);
        }
        if (healthpoints <= 0)
        {
            shop.updateGold(earn);
            GameObject gold = Instantiate(hp, GameObject.Find("Canvas").transform);
            gold.transform.position = Camera.main.WorldToScreenPoint(new Vector2(transform.position.x + 2 + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f)));
            gold.GetComponent<TextMeshProUGUI>().text = "+" + earn + " Gold";
            gold.GetComponent<TextMeshProUGUI>().color = Color.yellow;
            Destroy(gold, 1);

            Destroy(gameObject);
        }
    }

}

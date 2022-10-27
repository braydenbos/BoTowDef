using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public bool playerMode;
    public GameObject X;
    public GameObject selected;
    public Shop shop;
    public RectTransform target;
    public GameObject projectile;
    public Transform projectileParent;
    public float proSpeed;
    public int damage;
    public float radius;
    public int splashDamage;
    public float proLifeTime;
    public float timer;
    private float reloadTimer;
    public bool PartaclesOrTrail;
    public RectTransform bar;


    // Start is called before the first frame update
    void Start()
    {
        reloadTimer = timer;
        playerMode = false;
        X.SetActive(playerMode);
        target.gameObject.SetActive(playerMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Button();
        }
        if (playerMode)
        {
            target.position = Input.mousePosition;
            Vector3 Dir =(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.GetChild(0).position).normalized;
            Dir = new Vector3 (Dir.x, Dir.y,0);
            float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);
            if (Input.GetMouseButtonDown(0) && reloadTimer >= timer)
            {
                ShootAt(Dir);
            }
        }
        if(reloadTimer >= timer)
        {
            reloadTimer = timer;
        }
        else
        {
            reloadTimer += Time.deltaTime;
        }
        bar.localScale = new Vector3(reloadTimer / timer, 1, 0);
    }
    public void ShootAt(Vector3 predictionDir)
    {
        var Pro = Instantiate(projectile, projectileParent);
        Pro.transform.localScale = new Vector3(1, 1, 1);
        Pro.transform.position = transform.GetChild(0).GetChild(0).GetChild(0).position;
        Pro.GetComponent<Projectile>().Setup(predictionDir, proSpeed, damage, radius, splashDamage, proLifeTime, PartaclesOrTrail);
        reloadTimer = 0;
    }
    public void Button()
    {
        playerMode = !playerMode;
        if (playerMode)
        {
            shop.opened = false;
        }
        X.SetActive(playerMode);
        target.gameObject.SetActive(playerMode);
        selected.SetActive(playerMode);
        Cursor.visible = !playerMode;
    }
}

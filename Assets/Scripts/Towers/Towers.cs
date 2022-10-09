using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour
{
    //enemies
    public List<GameObject> enemies = new();
    //towerStats
    public float range;
    //_damage
    public int damage;
    //__splash
    public int splashDamage;
    public float radius;
    //_reload
    public float reloadSpeed;
    private float reloadTimer;
    //_projectile
    public float proSpeed;
    public float proLifeTime;
    //_shootStyle
    public bool smarttrack;
    //projectile
    public GameObject projectile;
    private Transform projectileParent;
    private void Start()
    {
        projectileParent = GameObject.Find("projectileParent").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            enemies.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            enemies.Remove(collision.gameObject);
        }
    }
    public void TowerUpdate()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = range;
    }
    private void Update()
    {
        if(enemies.Count > 0)
        {
            LookAt(CalcPosAtShot(enemies[0]));
        }

        reloadTimer += Time.deltaTime;
        if (enemies.Count > 0 && reloadTimer >= reloadSpeed)
        {
            if (smarttrack)
            {
                var Pro = Instantiate(projectile, projectileParent);
                Pro.transform.position = transform.position;
                Pro.GetComponent<Projectile>().SetupST(enemies[0], proSpeed, damage, radius, splashDamage, proLifeTime);
                reloadTimer = 0;
            }
            else
            {
                ShootAt(CalcPosAtShot(enemies[0]));
            }
        }
    }
    public void ShootAt(Vector3 predictionDir)
    {
        var Pro = Instantiate(projectile, projectileParent);
        Pro.transform.position = transform.position;
        Pro.GetComponent<Projectile>().Setup(predictionDir, proSpeed, damage, radius, splashDamage, proLifeTime);
        reloadTimer = 0;
    }
    public void LookAt(Vector3 predictionDir)
    {
        float angle = Mathf.Atan2(predictionDir.x, -predictionDir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    protected Vector3 CalcPosAtShot(GameObject monster)
    {
        // calculating the aim-direction
        EnemyMove move = monster.GetComponent<EnemyMove>();
        Vector3 monDir = move.UDir;
        Vector3 monPos = monster.transform.position;
        float monSpeed = move.movementSpeed;
        float dist = (monPos - transform.position).magnitude;
        float dur = dist / proSpeed;

        Vector3 max = monPos + monDir * monSpeed * dur;
        float timeDiff = CalcTimeDiff(move, max);

        Vector3 min = monPos + monDir * monSpeed * (dur + timeDiff);
        Vector3 middle = Vector3.Lerp(min, max, 0.5f);
        for (int i = 0; i < 100; i++)
        {
            float diff = Math.Abs(CalcTimeDiff(move, middle) - timeDiff);
            if (diff < 0.0001)
                break;

            timeDiff = CalcTimeDiff(move, middle);
            // overshot
            if (timeDiff < 0)
            {
                max = middle;
                middle = Vector3.Lerp(min, max, 0.5f);
            }
            // undershot
            else
            {
                min = middle;
                middle = Vector3.Lerp(min, max, 0.5f);
            }
        }


        Vector3 predictionDir = (middle - transform.position).normalized;

        // setup of the projectile

        return predictionDir;

        float CalcTimeDiff(EnemyMove mon, Vector3 bestPrediction) =>
            Vector3.Distance(transform.position, bestPrediction) / proSpeed -
            Vector3.Distance(mon.transform.position, bestPrediction) / mon.movementSpeed;
    }
}

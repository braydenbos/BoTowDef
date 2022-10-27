using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public GameObject poof;
    public float speed;
    public CircleCollider2D trigger;
    public int damage;
    public float radius;
    public bool splashed = false;
    public List<GameObject> inRadius = new();
    public int splashDamage;
    public bool smarttrack;
    private Vector3 curpos;
    private Vector3 lastpos;
    private float lifetime;
    private bool PartaclesOrTrail;

    public Vector3 UDir { get; private set; }
    private void Start()
    {
        if(PartaclesOrTrail && GetComponent<TrailRenderer>() != null)
        {
            Destroy(GetComponent<TrailRenderer>());
        }
        else if(GetComponent<ParticleSystem>() != null)
        {
            Destroy(GetComponent<ParticleSystem>());
        }
        trigger.radius = radius;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            inRadius.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            inRadius.Remove(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMove>() && collision.gameObject != null)
        {
            collision.gameObject.GetComponent<EnemyMove>().Damage(damage);
        }
        splashed = true;
    }
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0 || splashed)
        {
            Boom();
        }
        if (smarttrack && target != null)
        {
            SmartTrack();
            lastpos = curpos;
            curpos = transform.position;
        }
        else
        {
            if(UDir != Vector3.zero)
            {
                transform.Translate(speed * Time.deltaTime * UDir);
            }
            else
            {
                UDir = (curpos-lastpos).normalized;
            }
        }
    }
    public void SmartTrack()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
    public void Boom()
    {
        var Pof = Instantiate(poof, gameObject.transform.parent);
        Pof.transform.position = transform.position;
        Pof.GetComponent<Splash>().scale = radius * 2 * transform.localScale.y;
        for (int i = 0; i < inRadius.Count; i++)
        {
            inRadius[i].GetComponent<EnemyMove>().Damage(splashDamage);
        }
        Destroy(gameObject);
    }
    public void Setup(Vector3 uDir, float Speed, int Damage, float Radius, int SplashDam, float Lifetime,bool projectiletrail)
    {
        UDir = uDir.normalized;
        speed = Speed;
        damage = Damage;
        radius = Radius;
        splashDamage = SplashDam;
        lifetime = Lifetime;
        smarttrack = false;
        PartaclesOrTrail = projectiletrail;
    }
    public void SetupST(GameObject Tracked, float Speed, int Damage, float Radius, int SplashDam, float Lifetime,bool projectiletrail)
    {
        target = Tracked;
        speed = Speed;
        damage = Damage;
        radius = Radius;
        splashDamage = SplashDam;
        lifetime = Lifetime;
        smarttrack = true;
        PartaclesOrTrail = projectiletrail;
    }
}

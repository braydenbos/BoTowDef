using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [System.Serializable]
    public class Levels
    {
        [System.Serializable]
        public class Paths
        {
            public Vector2[] waypoints;// wpoints1
        }
        public Paths[] Path;
        public Sprite pathSprite;
    }
    public Levels[] level;
    public GameObject Lvl;
    private int pad;
    void Start()
    {
        pad = 0;// Random.Range(0, 2);
        GetComponent<SpriteRenderer>().sprite = level[pad].pathSprite;
        gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
        for (int i = 0; i < level[pad].Path.Length; i++)
        {
            GameObject wpoints = new(){ name = "wpoints" + i };
            wpoints.transform.parent = Lvl.transform;
            for( int j = 0; j < level[pad].Path[i].waypoints.Length; j++)
            {
                GameObject point = new(){ name = wpoints.name + "." + j };
                point.transform.parent = wpoints.transform;
                point.transform.position = level[pad].Path[i].waypoints[j];
            }
        }
    }
    public GameObject enemy;
    public void Spawnr()
    {
        int spawn = Random.Range(0, level[pad].Path.Length);
        GameObject en = Instantiate(enemy);
        en.transform.position = GameObject.Find("wpoints" + spawn).transform.GetChild(0).position;
        EnemyMove enen = en.GetComponent<EnemyMove>();
        enen.movementSpeed = 3;
        enen.maxhealthpoints = 10;
        enen.earn = 10;
        enen.path = spawn;
        enen.FindPath();
    }

}

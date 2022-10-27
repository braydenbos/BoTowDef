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
    public int pad;
    void Start()
    {
        pad = Random.Range(0, level.Length);
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
}

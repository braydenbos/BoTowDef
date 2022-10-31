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
        public Vector3[] starts;
        public Vector3[] ends;
        public Sprite pathSprite;
    }
    public Levels[] level;
    public GameObject Lvl;
    public Sprite arrow;
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
        for(int i = 0; i < level[pad].starts.Length; i++)
        {
            make(new Vector3(level[pad].starts[i].x, level[pad].starts[i].y, level[pad].starts[i].z+180), "start", new Color(255,255,0));
        }
        for (int i = 0; i < level[pad].ends.Length; i++)
        {
            make(level[pad].ends[i],"end",Color.red);
        }

    }
    public void make(Vector3 StartOrEnd,string se,Color color)
    {
        GameObject start = new() { name = se};
        start.AddComponent<SpriteRenderer>();
        start.GetComponent<SpriteRenderer>().sprite = arrow;
        start.GetComponent<SpriteRenderer>().sortingOrder = 3;
        start.transform.parent = GameObject.Find("map").transform;
        start.transform.Rotate(0, 0, StartOrEnd.z+90);
        start.transform.position = new Vector3(StartOrEnd.x, StartOrEnd.y, 0);
        start.transform.localScale = new Vector3(0.15f, 0.15f, 0);
        start.GetComponent<SpriteRenderer>().color = color;
    }
    public GameObject enemy;
}

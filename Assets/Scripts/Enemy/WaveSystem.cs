using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSystem : MonoBehaviour
{
    [System.Serializable]
    public class Enemys
    {
        public string name;
        public float movementSpeed;
        public int maxhealthpoints;
        public int earn;
        public Sprite Sprite;
        public Enemys[] enemys;
    }
    [System.Serializable]
    public class Paths
    {
        [System.Serializable]
        public class Waves
        {
            [System.Serializable]
            public class WaveEnemies
            {
                public int Enemy;
                public int Path;
            }
            public WaveEnemies[] enemies;
        }
        public Waves[] waves;
    }
    
    public Enemys[] enemys;
    public Paths[] paths;
    private int wave = -1;
    private int currentwave = -1;
    public Path Path;
    public GameObject enemy;
    public void Spawnr()
    {
        wave++;
        transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Wave: "+ (wave+1);
    }
    void Update()
    {
        if (wave > currentwave && wave <= paths[Path.pad].waves.Length)
        {
            for(int i = 0; i < paths[Path.pad].waves[wave].enemies.Length; i++)
            {
                GameObject enemi = Instantiate(enemy);
                enemi.transform.position = Path.level[Path.pad].Path[paths[Path.pad].waves[wave].enemies[i].Path].waypoints[0];
                Enemys enemystats = enemys[paths[Path.pad].waves[wave].enemies[i].Enemy];
                enemi.GetComponent<SpriteRenderer>().sprite = enemystats.Sprite;
                enemi.GetComponent<EnemyMove>().CopyStats(enemystats.movementSpeed, enemystats.maxhealthpoints, enemystats.earn, enemystats.enemys, paths[Path.pad].waves[wave].enemies[i].Path,i);
            }
            currentwave = wave;
        }
    }
}

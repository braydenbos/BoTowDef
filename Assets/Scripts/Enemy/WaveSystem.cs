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
    public Transform enemyparent;
    public Enemys[] enemys;
    public Paths[] paths;
    private int wave;
    private int currentwave;
    public Path Path;
    public GameObject enemy;
    public float earn;
    public Transform earntext;
    public Shop shop;
    public GameObject X;
    private void Start()
    {
        X.SetActive(false);
        wave = -1;
        currentwave = -1;
        earntext.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.yellow;
    }
    public void Spawnr()
    {
        int i = (int)Mathf.Round(earn);
        if (i > 0)
        {
            shop.updateGold(i);
        }
        if (wave+1 < paths[Path.pad].waves.Length)
        {
            wave++;
            transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Wave: " + (wave + 1);
        }
    }
    void Update()
    {
        print(paths[Path.pad].waves.Length);
        print(wave);
        if (wave > currentwave)
        {
            for(int i = 0; i < paths[Path.pad].waves[wave].enemies.Length; i++)
            {
                GameObject enemi = Instantiate(enemy);
                enemi.transform.parent = enemyparent;
                enemi.transform.position = Path.level[Path.pad].Path[paths[Path.pad].waves[wave].enemies[i].Path].waypoints[0];
                Enemys enemystats = enemys[paths[Path.pad].waves[wave].enemies[i].Enemy];
                enemi.GetComponent<SpriteRenderer>().sprite = enemystats.Sprite;
                enemi.GetComponent<EnemyMove>().CopyStats(enemystats.movementSpeed, enemystats.maxhealthpoints, enemystats.earn, enemystats.enemys, paths[Path.pad].waves[wave].enemies[i].Path,i);
            }
            currentwave = wave;
            earn = 400;
        }
        if(wave + 1 == paths[Path.pad].waves.Length)
        {
            X.SetActive(true);
            if (enemyparent.childCount == 0)
            {
                print("victory");
            }
        }
        if(earn > 0)
        {
            earn-=Time.deltaTime*10;
            int i = (int)Mathf.Round(earn);
            earntext.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Press next wave to earn";
            earntext.GetChild(1).GetComponent<TextMeshProUGUI>().text = i + System.Environment.NewLine + " gold";
        }
    }
}

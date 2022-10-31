using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health = 10;
    public GameObject DeadS;
    public WaveSystem waveSystem;
    public void Hit()
    {
        if (health >= 1)
        {
            transform.GetChild(10 - health).GetComponent<Animator>().SetTrigger("break");
            health--;
        }
        if (health < 1)
        {
            DeadS.SetActive(true);
            for(int i = 0; i < waveSystem.enemyparent.childCount; i++)
            {
                Destroy(waveSystem.enemyparent.GetChild(i).gameObject);
            }
        }
    }
}

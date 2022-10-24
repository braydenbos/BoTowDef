using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health = 10;
    public GameObject DeadS;
    public void Hit()
    {
        if (health >= 1)
        {
            transform.GetChild(10 - health).GetComponent<Animator>().SetTrigger("break");
            health--;
        }
        if (health < 2)
        {
            DeadS.SetActive(true);
        }
    }
}

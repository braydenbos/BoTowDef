using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public float movementTimer;
    private float timer;
    public void Restart()
    {

    }
    public void Quit()
    {

    }
    public void Update()
    {
        transform.parent.GetComponent<Image>().color = new Color(0, 0, 0,0.8f-((movementTimer - timer) / movementTimer));
        GetComponent<RectTransform>().localPosition = new Vector3(0, 2060 * (movementTimer - timer) / movementTimer,0);
        if (timer < movementTimer)
        {
            timer += Time.deltaTime;
        }
        else if (timer > movementTimer)
        {
            timer = movementTimer;
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public float scale;

    private void Start()
    {
        transform.localScale = new Vector2( scale, scale);

    }

    void Update()
    {
        if(transform.localScale.x < scale+1)
        {
            transform.localScale += new Vector3(3, 3, 3) * Time.deltaTime;
            SpriteRenderer sR = GetComponent<SpriteRenderer>();
            sR.color = new Color(sR.color.r, sR.color.g, sR.color.b, sR.color.a - 3* Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    Vector3 mOgPos;
    Vector3 ogPos;
    Vector3 mCurPos;
    Vector4 MaxMinpos;
    new Camera camera;
    public Vector2 scrollMinMax;
    public float scrollSpeed;
    private void Start()
    {
        camera = GetComponent<Camera>();
    }
    void Update()
    {
        ScrollInBounds();
        float mS = Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed;
        if (mS != 0 && camera.orthographicSize - mS > scrollMinMax.x && camera.orthographicSize - mS < scrollMinMax.y)
        {
            camera.orthographicSize -= mS;
        }
        MaxMinpos.x = 11 - camera.orthographicSize * 1.9f;
        MaxMinpos.y = 6 - camera.orthographicSize;
        MaxMinpos.z = -11 + camera.orthographicSize * 1.9f;
        MaxMinpos.w = -6 + camera.orthographicSize;

        if (Input.GetMouseButtonDown(2))
        {
            mOgPos = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            ogPos = transform.position;
        }
        else if (Input.GetMouseButton(2))
        {
            mCurPos = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.position = InBounds();
        }
    }
    public Vector3 InBounds()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if (ogPos.x - (mCurPos.x - mOgPos.x)<MaxMinpos.x&& ogPos.x - (mCurPos.x - mOgPos.x) > MaxMinpos.z)
        {
            x = ogPos.x - (mCurPos.x - mOgPos.x);
        }
        if (ogPos.y - (mCurPos.y - mOgPos.y) < MaxMinpos.y && ogPos.y - (mCurPos.y - mOgPos.y) > MaxMinpos.w)
        {
            y = ogPos.y - (mCurPos.y - mOgPos.y);
        }
        return new Vector3(x, y, -10);
    }
    public void ScrollInBounds()
    {
        if(transform.position.x > 11 - camera.orthographicSize * 1.9f)
        {
            transform.position += new Vector3(11 - camera.orthographicSize * 1.9f - transform.position.x, 0, 0);
        }
        else if(transform.position.x < -11 + camera.orthographicSize * 1.9f)
        {
            transform.position += new Vector3(-11 + camera.orthographicSize * 1.9f - transform.position.x, 0, 0);
        }
        if ( transform.position.y > 6 - camera.orthographicSize)
        {
            transform.position += new Vector3(0, 6 - camera.orthographicSize - transform.position.y, 0);
        }
        else if (transform.position.y < -6 + camera.orthographicSize)
        {
            transform.position += new Vector3(0, -6 + camera.orthographicSize - transform.position.y, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            transform.position = touch.position;
        }
        if (Application.isEditor)
        {
            transform.position = Input.mousePosition;
        }
    }

}


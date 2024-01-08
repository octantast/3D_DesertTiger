using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSprite : MonoBehaviour
{
   // public Vector3 targetPosition;
    public float speed;

    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed);
    }
}

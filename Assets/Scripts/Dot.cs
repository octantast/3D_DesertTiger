using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public GeneralController general;
 public void Pressed()
    {
        int count = 0;
        foreach (Transform child in transform.parent.transform)
        {
            if (child.gameObject.activeSelf)
            {
                count++;
            }
        }
        if(count <= 1)
        {
            // win
            general.win = true;
        }
        this.gameObject.SetActive(false);
    }
}

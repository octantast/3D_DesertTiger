using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchAuto : MonoBehaviour
{
    [HideInInspector] public GeneralController general;
    [HideInInspector] public SphereCollider catcher;
    private void OnTriggerEnter(Collider other)
    {
        if (catcher == enabled)
        {
            if (other.CompareTag("Bug") || other.CompareTag("BugGreen") || other.CompareTag("BugViolet"))
            {
                if (general.hitObject != null)
                {
                    Destroy(general.hitObject); // destroys old
                }
                general.hitObject = other.gameObject;
                general.hitObject.transform.SetParent(general.playerGo.transform);
                general.flyTapped(other.tag);
                Debug.Log("Cathed" + other.tag);
            }
        }
    }
}

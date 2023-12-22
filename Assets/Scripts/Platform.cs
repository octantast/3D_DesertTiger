using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GeneralController generalController;
    public GameObject parentPlatform;

    public float violetFlyRarity;
    public float greenFlyRarity;

    public GameObject flyPrefab; // casual
    public GameObject flyPrefab3; // rare
    public GameObject flyPrefab2; // legend
    public GameObject flyHolder;
    [HideInInspector] public float flyXposition = 75;
    private GameObject chosenFly;

    public GameObject cactusyHolder;
    public List<GameObject> cactusy;
    private int randomiser;


    public bool playerTouched;
    private void Start()
    {
        foreach (Transform child in cactusyHolder.transform)
        {
            cactusy.Add(child.gameObject);
        }
        spawnCactuses();
        spawnFlies();
    }

    private void Update()
    {
        parentPlatform.transform.localPosition -= new Vector3(0, 0, 1) * generalController.platformspeed * Time.deltaTime * generalController.platformIndex;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !playerTouched)
        {
            playerTouched = true;
            if (generalController.lastPlatform != null)
            {
                generalController.lastPlatform.playerTouched = false;
                generalController.lastPlatform.parentPlatform.transform.localPosition = new Vector3(0, 0, generalController.lastPlatform.parentPlatform.transform.localPosition.z + 390);

                // respawn everything

                generalController.lastPlatform.flyXposition = 75;
                generalController.lastPlatform.spawnFlies();
                generalController.lastPlatform.spawnCactuses();

            }
            generalController.lastPlatform = this;
        }
    }

    public void spawnCactuses()
    {
        foreach (GameObject child in cactusy)
        {
            child.SetActive(false);
        }

        for (int i = 0; i < cactusy.Count; i++)
        {
            int num = Random.Range(0, 20);
            if(num > 10)
            {
                cactusy[i].SetActive(true);
            }
        }
    }

    public void spawnFlies()
    {
        foreach (Transform child in flyHolder.transform)
        {
            Destroy(child.gameObject);
        }
        StartCoroutine("SpawnFly");
    }

    IEnumerator SpawnFly()
    {
        while (flyXposition > -25)
        {
            flyXposition -= Random.Range(15, 30);
            if (generalController.catchFly == 0)
            {
                randomiser = 99;
            }
            else
            {
                randomiser = Random.Range(0, 100);
            }
            if(randomiser < violetFlyRarity)
            {
                chosenFly = Instantiate(flyPrefab2, transform.position, Quaternion.identity, flyHolder.transform);
            }
            else if (randomiser > violetFlyRarity && randomiser < greenFlyRarity)
            {
                chosenFly = Instantiate(flyPrefab3, transform.position, Quaternion.identity, flyHolder.transform);
            }
            else
            {
                chosenFly = Instantiate(flyPrefab, transform.position, Quaternion.identity, flyHolder.transform);
            }

            if (generalController.catchFly == 0)
            {
                randomiser = 1;
            }
            else
            {
                randomiser = Random.Range(0, 2);
            }

            switch (randomiser)
            {
                case 0:
                    chosenFly.transform.localPosition = new Vector3(flyXposition, 0, -5f);
                    break;
                default:
                    chosenFly.transform.localPosition = new Vector3(flyXposition, 0, -2.7f);
                    break;
            }

            yield return null;
        }
    }
}

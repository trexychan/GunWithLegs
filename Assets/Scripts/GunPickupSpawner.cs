using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupSpawner : MonoBehaviour
{
    List<GunPickup> listOfPickups;
    // Start is called before the first frame update
    void Start()
    {
        listOfPickups = new List<GunPickup>(FindObjectsOfType<GunPickup>());
        foreach (GunPickup pickup in listOfPickups) {
            if (PlayerData.Instance.gunList.Contains((int)pickup.gunType) == false) {
                pickup.gameObject.SetActive(true);
            } else {
                pickup.gameObject.SetActive(false);
            }
        }

        AirJordans ajs = FindObjectOfType<AirJordans>();
        if (PlayerData.Instance.hasAirJorguns)
        {
            ajs.gameObject.SetActive(false);
        }
    }
}

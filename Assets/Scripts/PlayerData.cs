using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    private static PlayerData instance;

    public static PlayerData Instance { get { return instance; } }

    public List<int> gunList;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
            gunList = new List<int>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void addGun(int gunType) {
        gunList.Add(gunType);
    }
}

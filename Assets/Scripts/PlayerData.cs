using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    private static PlayerData instance;

    public static PlayerData Instance { get { return instance; } }

    public List<int> gunList;

    public Vector3 player_position;
    public bool hasAirJorguns;
    public float player_current_health;
    public float player_max_health;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
            gunList = new List<int>();
            hasAirJorguns = false;
            player_current_health = 9f;
            player_max_health = player_current_health;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void addGun(int gunType) {
        gunList.Add(gunType);
    }

    public void SetPlayerPosition(Vector3 in_position)
    {
        player_position = in_position;
    }

    public void SetPlayerHealth(float curr, float max)
    {
        player_current_health = curr;
        player_max_health = max;
    }

    public void obtainAirJorguns() {
        hasAirJorguns = true;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    private static PlayerData instance;

    public static PlayerData Instance { get { return instance; } }

    public List<int> gunList;

    public Vector3 player_position;
    public int player_current_health;
    public int player_max_health;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
            gunList = new List<int>();
            player_position = new Vector3(-19f, 0.5f, 0f);
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

    public void SetPlayerHealth(int curr, int max)
    {

    }
}

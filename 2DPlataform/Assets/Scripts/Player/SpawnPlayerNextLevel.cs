using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerNextLevel : MonoBehaviour
{
    private Transform Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        Player.position = transform.position;

    }


}

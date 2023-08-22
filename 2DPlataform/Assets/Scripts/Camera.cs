using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform Player;

    [SerializeField] private float Vel;
    [SerializeField] private float OffsetY;
    [SerializeField] private float OffsetZ;
    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void LateUpdate()
    {
        Vector3 pos = new Vector3(Player.position.x,Player.position.y + OffsetY,Player.position.z - OffsetZ);
        transform.position = Vector3.Slerp(transform.position, pos, Vel);
    }
}

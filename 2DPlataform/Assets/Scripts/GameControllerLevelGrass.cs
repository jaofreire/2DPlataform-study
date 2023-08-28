using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerLevelGrass : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera PlayerCamera;
    [SerializeField] private PolygonCollider2D Confiner;
    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        PlayerCamera.Follow = Player.transform;
        PlayerCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = Confiner;
    }

   
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera PlayerCamera;
    [SerializeField] private CinemachineVirtualCamera[] Cameras;

    void Start()
    {
        SwitchCam(PlayerCamera);
    }

    void SwitchCam(CinemachineVirtualCamera TargetCamera)
    {
        foreach (CinemachineVirtualCamera camera in Cameras)
        {
            camera.enabled = camera == TargetCamera;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BossCamera"))
        {
            CinemachineVirtualCamera targetCamera = collision.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchCam(targetCamera);
        }
    }

    private void OnTriggerExit2D(Collider2D collisionExit)
    {
        if (collisionExit.gameObject.CompareTag("BossCamera"))
        {
            SwitchCam(PlayerCamera);
        }

    }

    

}

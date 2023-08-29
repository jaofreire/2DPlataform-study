using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private List<Transform> CheckPoints = new List<Transform>();
    private Transform CurrentCheckPoint;
    private Animator AnimationCheckPoint;
    private bool CanSave;

    public static CheckPoint instance;
    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CanSave = true;
        }
        else
        {
            CanSave = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Save"))
        {
            collision.gameObject.GetComponent<Transform>();

            if (!CheckPoints.Contains(collision.gameObject.transform))
            {
                CheckPoints.Add(collision.transform);
            }
         
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Save") && CheckPoints.Contains(collision.gameObject.transform))
        {
            if (CanSave)
            {
                CurrentCheckPoint = collision.gameObject.GetComponent<Transform>();
                AnimationCheckPoint = collision.gameObject.GetComponent<Animator>();
                AnimationCheckPoint.SetTrigger("Save");
                //Debug.Log("check point atual é:" + CurrentCheckPoint.name);
            }
        }
    }

    public void ReturnCheckPoint()
    {
        transform.position = CurrentCheckPoint.position;
    }

    

}

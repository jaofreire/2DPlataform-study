using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float Time;
    private Animator Animation;

    public static Door instance;
    private void Start()
    {
        Animation = GetComponent<Animator>();
        StartCoroutine(DoorAnimTime());

        instance = this;
    }


    IEnumerator DoorAnimTime()
    {
        yield return new WaitForSeconds(Time);
        Animation.SetTrigger("DoorLight");
        StartCoroutine(DoorAnimTime());
    }

    public void ActiveDoor()
    {
        Animation.SetBool("DoorDown", true);
        StopAllCoroutines();
    }
 
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour,IProps
{
    private Animator Animation;

    private void Start()
    {
        Animation = GetComponent<Animator>();
    }

    public void Interactive()
    {
        Animation.SetTrigger("Break");
        Destroy(gameObject, 3f);
    }
  
}

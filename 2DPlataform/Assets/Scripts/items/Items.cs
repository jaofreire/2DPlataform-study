using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private int healValue;
    [SerializeField] public int TotalLifePlayer;

    public static Items instance;
    private void Start()
    {
        instance = this;
    }
    void Heal(int HealValue)
    {
        
        if (PlayerMovement.instance.Life < TotalLifePlayer)
        {
            Debug.Log("Curou");
            PlayerMovement.instance.Life += HealValue;
        }
        else
        {
            Debug.Log("vida cheia, nao precisa curar");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LifePotion"))
        {
            Heal(healValue);
            Destroy(collision.gameObject);
        }
    }



}

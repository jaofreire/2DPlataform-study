using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D Rig;
    private Animator Animation;

    [SerializeField] private float Vel;
    [SerializeField] private float X;
    [SerializeField] private float Y;
    [SerializeField] private int Damage;
    [SerializeField] private GameObject ExplosionFx;


    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        Animation = GetComponent<Animator>();

        Throw();
    }

    void Throw()
    {
       
        if (BomberGoblin.instance.IsRight)
        {
            Rig.AddForce(new Vector2(X, Y), ForceMode2D.Impulse);
        }
        else
        {
            Rig.AddForce(new Vector2(-X, Y), ForceMode2D.Impulse);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
           GameObject explosion = Instantiate(ExplosionFx, transform.position, transform.rotation);
            Destroy(explosion, 1f);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 8)
        {
            PlayerMovement.instance.OnHit(Damage);
            GameObject explosion = Instantiate(ExplosionFx, transform.position, transform.rotation);
            Destroy(explosion, 1f);
            Destroy(gameObject);
        }
       
    }

}

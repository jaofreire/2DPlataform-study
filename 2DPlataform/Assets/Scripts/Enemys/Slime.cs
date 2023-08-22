using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private Rigidbody2D Rig;
    private Animator Animation;
   
    [SerializeField] private float MoveSpd;

    [SerializeField] private int Life;
    [SerializeField] private int Damage;

    [SerializeField] private Transform PointCollider;
    [SerializeField] private float Radius;
    [SerializeField] private LayerMask Layer;

    

    // Start is called before the first frame update
    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        Animation = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveOnCollider();
    }

    void MoveOnCollider()
    {
        Rig.velocity = new Vector2(-MoveSpd, Rig.velocity.y);

        Collider2D collider = Physics2D.OverlapCircle(PointCollider.position, Radius, Layer);

        if (collider != null)
        {
            float rotY = transform.rotation.y;
            MoveSpd = -MoveSpd;

            if (rotY == 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    public void OnHit(int damage)
    {
        Animation.SetTrigger("Hit");
        Life -= damage;

        if (Life <= 0)
        {
            MoveSpd = 0;
            Animation.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
        Debug.Log("Atacou");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointCollider.position, Radius);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement.instance.OnHit(Damage);
            
        }
    }
}

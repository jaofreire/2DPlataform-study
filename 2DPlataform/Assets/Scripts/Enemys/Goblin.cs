using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private Rigidbody2D Rig;
    private Animator Animation;

    [SerializeField] private int Life;

    [Header("Move")]
    [SerializeField] private float MoveSpd;
    [SerializeField] private float StopDistance;
    private bool IsMoving;



    [Header("Vision")]
    [SerializeField] private bool IsRight;
    [SerializeField] private float RayReach;
    [SerializeField] private Transform RayPoint;
    [SerializeField] private LayerMask PlayerLayer;
    private Vector2 Direc;

    [Header("Attack")]
    [SerializeField] private Transform HitBox;
    [SerializeField] private float Radius;
    [SerializeField] private int Damage;
    

    void Start()
    {
        Animation = GetComponent<Animator>();
        Rig = GetComponent<Rigidbody2D>();

    }

   
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        VisionReach();
        OnMove();
    }

    void OnMove()
    {
        if (IsRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            Direc = Vector2.right;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            Direc = Vector2.left;
        }
       if (IsMoving)
       { 
        
         if (IsRight)
         {
            Rig.velocity = new Vector2(MoveSpd, Rig.velocity.y);
         }
         else
         {
            Rig.velocity = new Vector2(-MoveSpd, Rig.velocity.y);
         }
       }
    }
    

    void VisionReach()
    {
        RaycastHit2D hit = Physics2D.Raycast(RayPoint.position, Direc, RayReach);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                IsMoving = true;
                Debug.Log("É um Player");

                float Distance = Vector2.Distance(transform.position, hit.transform.position);

                if (Distance <= StopDistance)
                {
                    Debug.Log("Encostou");
                    IsMoving = false;
                    Rig.velocity = Vector2.zero;
                    OnAttack();
                }
            }
        }
    }

    void OnAttack()
    {
        Collider2D Hitbox = Physics2D.OverlapCircle(HitBox.position, Radius, PlayerLayer);

        if (HitBox != null)
        {
            PlayerMovement.instance.OnHit(Damage);
        }
    }

    public void OnHit(int damage)
    {
        Life -= damage;

        if (Life <= 0)
        {
            //die;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(RayPoint.position, Direc * RayReach);
        Gizmos.DrawWireSphere(HitBox.position, Radius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement.instance.OnHit(Damage);
        }
    }
}

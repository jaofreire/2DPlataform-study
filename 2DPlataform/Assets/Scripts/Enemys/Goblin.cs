using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goblin : MonoBehaviour, IHit
{
    private Rigidbody2D Rig;
    private Animator Animation;

    [SerializeField] private int Life;
    [SerializeField] private GameObject Drop;

    [Header("Move")]
    [SerializeField] private float MoveSpd;
    [SerializeField] private float StopDistance;
    [SerializeField] private bool IsRight;
    [SerializeField] private bool IsIdle;
    private bool IsMoving;



    [Header("Vision")]
    [SerializeField] private float RayReach;
    [SerializeField] private Transform RayPoint;
    [SerializeField] private Transform RayPointBehind;
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
            Animation.SetInteger("Transition", 1);

            if (IsRight)
            {
                Rig.velocity = new Vector2(MoveSpd, Rig.velocity.y);
            }
            else if (!IsRight)
            {
                Rig.velocity = new Vector2(-MoveSpd, Rig.velocity.y);
            }

            if (IsIdle)
            {
                Animation.SetInteger("Transition", 0);
                Rig.velocity = Vector2.zero;
                IsMoving = false;
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
                IsIdle = false;
                IsMoving = true;
                //Debug.Log("É um Player");

                float Distance = Vector2.Distance(transform.position, hit.transform.position);

                if (Distance <= StopDistance)
                {
                    Animation.SetInteger("Transition", 2);
                    //Debug.Log("Encostou");
                    IsMoving = false;
                    Rig.velocity = Vector2.zero;
                    OnAttack();
                }
            }
        }
        else
        {
            IsIdle = true;
        }


        RaycastHit2D HitBehind = Physics2D.Raycast(RayPointBehind.position, -Direc, RayReach);
        

        if (HitBehind.collider != null)
        {
            IsIdle = false;
            IsMoving = true;
            if (HitBehind.transform.CompareTag("Player"))
            {
                //Debug.Log("ATRAS!!");
                IsRight = !IsRight;
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
        Animation.SetTrigger("Hit");
        Life -= damage;
        
        if (Life <= 0)
        {
            Animation.SetTrigger("Death");
            Destroy(gameObject, 1f);

            int prob = Random.Range(0, 5);
         
            if (prob >= 3)
            {
                DropItem();
                Debug.Log("Drop");
            }
        }
    }

    void DropItem()
    {
        Instantiate(Drop, transform.position, transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(RayPoint.position, Direc * RayReach);
        Gizmos.DrawRay(RayPointBehind.position, -Direc * RayReach);
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

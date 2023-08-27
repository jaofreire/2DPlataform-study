using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberGoblin : MonoBehaviour, IHit
{
    private Animator Animation;

    [SerializeField] private int Life;

    [Header("Attack")]
    [SerializeField] private Transform SpawnBomb;
    [SerializeField] private GameObject Bomb;
    [SerializeField] private float Time;
    [SerializeField] private int Damage;
    private bool CanAttack = true;

    [Header("Direction")]
    [SerializeField] private Transform RayPoint;
    [SerializeField] private Transform RayPointBehind;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private float RayDistance;
    public bool IsRight;
    private Vector2 Direc;

    public static BomberGoblin instance;

    void Start()
    {
        Animation  = GetComponent<Animator>();

        instance = this;
    }

    
    void Update()
    {
       ActiveAttack();
        
    }

    private void FixedUpdate()
    {
        ReachPlayer();
    }

    public void OnHit(int damage)
    {
        Animation.SetTrigger("Hit");
        Life -= damage;

        if (Life <= 0)
        {
            Animation.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
    }

    void ReachPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(RayPoint.position, Direc, RayDistance, PlayerLayer);

        if (hit.collider != null)
        {
            IsRight = true;  
        }


        RaycastHit2D hitBehind = Physics2D.Raycast(RayPointBehind.position, -Direc, RayDistance, PlayerLayer);
        if (hitBehind.collider != null)
        {
            IsRight = false;
        }

    }

   
    void ActiveAttack()
    {


        if ( IsRight)
        {
            Direc = Vector2.right;
            transform.eulerAngles = new Vector3(0, 0, 0);
            
            if (CanAttack)
            {
                Animation.SetInteger("Transition", 1);
            }   
        }
        else if (!IsRight)
        {
           
            transform.eulerAngles = new Vector3(0, 180, 0);
            
            if (CanAttack)
            {
                Animation.SetInteger("Transition", 1);
            } 
        }
        else
        {
            Animation.SetInteger("Transition", 0);
        }
        
    }
    void Attack()
    {
        if (CanAttack)
        {
            Instantiate(Bomb, SpawnBomb.position, SpawnBomb.rotation);
            CanAttack = false;
            StartCoroutine(AttackTime());
        }
    }

    IEnumerator AttackTime()
    {
        Animation.SetInteger("Transition", 0);
        yield return new WaitForSeconds(Time);
        CanAttack = true;
        Animation.SetInteger("Transition", 1);
        StopCoroutine(AttackTime());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(RayPoint.position, Direc * RayDistance);
        Gizmos.DrawRay(RayPointBehind.position, -Direc * RayDistance);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            PlayerMovement.instance.OnHit(Damage);
        }
    }

}

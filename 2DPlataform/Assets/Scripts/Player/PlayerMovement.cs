using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator Animation;
    [SerializeField] private BoxCollider2D Collider;
    private Rigidbody2D Rig;
    

    [SerializeField] private int Life;

    [Header("Move")]
    [SerializeField] private float MoveSpd;
    [SerializeField] private float JumpSpd;
    [SerializeField] private LayerMask LayerGround;
    private bool IsGround;
    private int JumpTimes;
    private float DirX;

    [Header("Attack")]
    [SerializeField] private Transform HitBoxPoint;
    [SerializeField] private float Radius;
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private float HitCount;
    [SerializeField] private int Damage;
    private bool CanHit = true;
    private bool IsAttacking;

    public static PlayerMovement instance;

    void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        Rig = GetComponent<Rigidbody2D>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {

        DirX = Input.GetAxisRaw("Horizontal");
        Rig.velocity = new Vector2(DirX * MoveSpd, Rig.velocity.y);

        switch (DirX)
        {
            default:
                if (!IsAttacking && IsGround)
                {
                    Animation.SetInteger("Transition", 0);
                }
                break;

            case > 0:
                if (!IsAttacking)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                if ( !IsAttacking && IsGround)
                {
                    Animation.SetInteger("Transition", 1);
                }
                break;

            case < 0:
                if (!IsAttacking)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                if ( !IsAttacking && IsGround)
                {
                    Animation.SetInteger("Transition", 1);
                }
                break;
        }

    }

    void Jump()
    {
        bool JumpButton = Input.GetButtonDown("Jump");


        if (JumpButton && JumpTimes == 2 && IsGround)
        {
            JumpTimes = 1;
            IsGround = false;
            Animation.SetInteger("Transition", 2);
            Rig.AddForce(Vector2.up * JumpSpd, ForceMode2D.Impulse);
            //Debug.Log(JumpTimes);
            //if (JumpTimes == 1)
            //{
            //    Debug.Log("DoubleJump");
            //}
        }
        else if (JumpButton && JumpTimes == 1)
        {
            Animation.SetInteger("Transition", 2);
            Rig.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
            JumpTimes = 0;
            //Debug.Log(JumpTimes);
        }

    }

    void Attack()
    {
        bool AttackButton = Input.GetKeyDown(KeyCode.K);
        
        if (AttackButton && !IsAttacking )
        {
            MoveSpd = 0;
            DirX = 0;
            IsAttacking = true;

            Animation.SetInteger("Transition", 3);

            Collider2D hit = Physics2D.OverlapCircle(HitBoxPoint.position, Radius, EnemyLayer);

            if (hit != null)
            {
                IHit HitEnemy = hit.transform.GetComponent<IHit>();

                if (HitEnemy == null) return;

                HitEnemy.OnHit(Damage);

            }
          
            StartCoroutine(OnAttack());
        }
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(0.3f);
        IsAttacking = false;
        MoveSpd = 6;
        DirX = Input.GetAxisRaw("Horizontal");
        StopCoroutine(OnAttack());
    }

    public void OnHit(int damage)
    {
        if (CanHit)
        {
            Debug.Log("Levou dano");

            Animation.SetTrigger("Hit");
            Life -= damage;
            CanHit = false;
            StartCoroutine(HitTimeCount());

            if (Life <= 0)
            {
                Animation.SetTrigger("Death");
            }
        }
    }

    IEnumerator HitTimeCount()
    {
        yield return new WaitForSeconds(HitCount);
        CanHit = true;
        Debug.Log("Posso levar dano novamente");
        StopCoroutine(HitTimeCount());
    }




    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(HitBoxPoint.position, Radius);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            collision.GetComponent<Animator>().SetTrigger("PickUp");
            Destroy(collision.gameObject, 0.5f);
            GameController.instance.GetCoin();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            IsGround = true;
            JumpTimes = 2;
        }

    }
}

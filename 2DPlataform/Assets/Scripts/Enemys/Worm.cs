using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour, IHit
{
    private Animator Animation;

    [SerializeField] private float MoveSpd;
    [SerializeField] private GameObject[] WayPoints;
    private int NextWayPoint = 1;
    private float DistancePoint;

    [SerializeField] private int Damage;
    [SerializeField] private int Life;

  
    void Start()
    { 
        Animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    Move();
    //}

    private void FixedUpdate()
    {
        Move();
    }

    public void OnHit(int Damage)
    {
        Animation.SetTrigger("Hit");
        Life -= Damage;

        if (Life <= 0)
        {
            Animation.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }

    }


    void Move()
    {
        DistancePoint = Vector2.Distance(transform.position, WayPoints[NextWayPoint].transform.position);

        transform.position = Vector2.MoveTowards(transform.position, WayPoints[NextWayPoint].transform.position, MoveSpd * Time.deltaTime);

        if (DistancePoint < 0.2f)
        {
            ChangeDirection();
        }

    }

    void ChangeDirection()
    {
        Vector3 CurrentDirec = transform.eulerAngles;
        CurrentDirec.z += WayPoints[NextWayPoint].transform.eulerAngles.z;
        transform.eulerAngles = CurrentDirec;
        ChooseNewNextWayPoint();
    }

    void ChooseNewNextWayPoint()
    {
        NextWayPoint++;

        if (NextWayPoint == WayPoints.Length)
        {
            NextWayPoint = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            PlayerMovement.instance.OnHit(Damage);
        }
    }

}

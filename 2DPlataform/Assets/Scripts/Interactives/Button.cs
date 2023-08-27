using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] public BoxCollider2D BoxCollider;
    [SerializeField] private LayerMask PlayerMask;
    private Animator Animation;
    private bool IsPressed;
    private GameObject Player;
    private float ColliderDistance;

    void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        Animation = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        OnPressed();
        CheckCollision();

    }


    public void OnPressed()
    {
        if (IsPressed)
        {
            Animation.SetBool("Pressed", true);
        }
        else
        {
            Animation.SetBool("Pressed", false);
        }
    }

    void CheckCollision()
    {
        RaycastHit2D hit = Physics2D.BoxCast(BoxCollider.bounds.center, BoxCollider.bounds.size, 0f, Vector2.up, ColliderDistance, PlayerMask);

        if(hit.collider != null)
        {
            IsPressed = true;
            Door.instance.ActiveDoor();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(BoxCollider.bounds.center, BoxCollider.bounds.size);
    }

}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlataform : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveX;
    [SerializeField] private float moveY;
    [SerializeField] private float Duration;

    public static MovePlataform instance;
    private void Start()
    {
        instance = this;
    }

    public void MoveY()
    {
        transform.DOMove(new Vector2(moveX, moveY), Duration);
    }
    public void MoveX()
    {
        transform.DOMove(new Vector2(moveX, moveY), Duration);
    }


}

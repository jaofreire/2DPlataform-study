using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemLootBox : MonoBehaviour, IProps
{
    private Animator Animation;

    [SerializeField] private List<GameObject> ItemsList = new List<GameObject>();
    [SerializeField] private float X;
    [SerializeField] private float Y;
    [SerializeField] private float Duration;

    [SerializeField] private Transform SpawnItem;

    [SerializeField] private bool CanOpen;

    void Start()
    {
        Animation = GetComponent<Animator>();
    }


    public void Interactive()
    {
        if (CanOpen)
        {
            int random = Random.Range(0, ItemsList.Count);
            GameObject item = Instantiate(ItemsList[random], SpawnItem.position, SpawnItem.rotation);
            item.transform.DOMove(new Vector2(X, Y), Duration);
            Animation.SetTrigger("Open");
            CanOpen = false;
        }
    }
}

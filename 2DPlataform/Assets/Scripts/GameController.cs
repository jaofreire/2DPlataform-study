using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private int CoinCount;
    [SerializeField] private Text CoinText;
 
    void Awake()
    { 
        instance = this;
    }

    public void GetCoin()
    {
        CoinCount++;
        CoinText.text = "Coins: " + CoinCount;
    }
}

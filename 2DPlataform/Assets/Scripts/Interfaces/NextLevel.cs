using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour, IProps
{
    [SerializeField] private int levelNumber;
    private bool CanNextLevel;

    public void Interactive()
    {
        //bool ButtonNextLevel = Input.GetKeyDown(KeyCode.K);

        if (CanNextLevel)
        {
            SceneManager.LoadScene(levelNumber);
        }
      
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            CanNextLevel = true;
            Debug.Log("Posso ir para proxima fase");
        }
    }
    void OnTriggerExit2D(Collider2D collisionExit)
    {
        if (collisionExit.gameObject.layer == 8)
        {
            CanNextLevel = false;
            Debug.Log(" Não posso ir para proxima fase");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    //Serialized Fields
    [SerializeField]
    [Min(0)]
    float delayBeforeSceneReload = 2f;

    [SerializeField]
    [Min(1)]
    int maxLives = 3;

    [SerializeField] 
    Vector3 startPos;

    int lives;
    //Property for lives
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            if (value < lives) //Player took damage
            {
                lives = value;
                Debug.Log($"Remaining Lives: {lives}");
                if (lives <= 0)
                {
                    StartCoroutine(GameOver());
                }
                else
                {
                    StartCoroutine(KillPlayer());
                }
            }
            else //Player gained health
            {
                lives = value;
                //Gain health logic here
            }
        }
    }

    //Cashe References

    //Attribute
    bool resetLives = true;

    //Delegate for resetting the player
    public delegate void NotifyPlayerReset();
    public static NotifyPlayerReset playerReset;

    private void Awake()
    {
        playerReset += ResetPlayerObject;

        transform.position = startPos;

        if (resetLives)
        {
            lives = maxLives;
            resetLives = false;
        }
    }

    IEnumerator KillPlayer()
    {
        Player.instance.GetComponentInChildren<Renderer>().enabled = false;
        //Wait to reload scene
        yield return new WaitForSeconds(delayBeforeSceneReload);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerReset?.Invoke();
    }

    IEnumerator GameOver()
    {
        Debug.Log("Game Over");
        //Game over logic here
        resetLives = true;
        yield return null;
    }
    
    void ResetPlayerObject()
    {
        Player.instance.GetComponentInChildren<Renderer>().enabled = true;
        Player.instance.transform.position = startPos;
    }
}

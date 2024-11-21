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


    //Attributes
    Vector3 startPos;
    bool resetLives = true;

    private void Awake()
    {
        startPos = transform.position;

        if (resetLives)
        {
            lives = maxLives;
            resetLives = false;
        }
    }

    IEnumerator KillPlayer()
    {
        gameObject.GetComponentInChildren<Renderer>().enabled = false;
        //Wait to reload scene
        yield return new WaitForSeconds(delayBeforeSceneReload);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResetPlayerObject();
    }

    IEnumerator GameOver()
    {
        Debug.Log("Game Over");
        //Game over logic here
        resetLives = true;
        yield return null;
    }
    
    //This method is similar to method in player mover. May turn into delegate or event. For now, this'll suffice.
    void ResetPlayerObject()
    {
        GetComponent<PlayerMover>().ResetPlayer();
        gameObject.GetComponentInChildren<Renderer>().enabled = true;
        transform.position = startPos;
    }
}

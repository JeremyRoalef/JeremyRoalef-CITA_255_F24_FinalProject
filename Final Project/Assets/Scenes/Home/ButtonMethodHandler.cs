using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMethodHandler : MonoBehaviour
{
    string GAME_SCENE_NAME = "Main";

    //Button Methods
    public void OnButtonStartGameClick()
    {
        SceneManager.LoadScene(GAME_SCENE_NAME);
    }
    public void OnButtonQuitGameClick()
    {
        Debug.Log("Quitting Game");
    }

}

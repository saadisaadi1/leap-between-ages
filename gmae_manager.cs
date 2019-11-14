using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gmae_manager : MonoBehaviour
{
    public float time_between_loading;
    public int first_play_scene;
    public int last_play_scene;
   
    
    public void checkScore(int score)
    {
        for(int i = first_play_scene ; i <= last_play_scene; i++)
        {
            if(SceneManager.GetActiveScene().buildIndex == i)
            {
                if (score >= 40 * (i - first_play_scene + 1))
                {
                    waitAndLoadNext(1);
                }
            }
        }
       

    }
    public void waitAndLoadNext(int i)
    {
        if (i == 1)
        {
            Invoke("loadNextScene", time_between_loading);
        }
        if (i == 2)
        {
            Invoke("restart", time_between_loading);
        }
    }
    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void restart()
    {
        if ((SceneManager.GetActiveScene().buildIndex >= first_play_scene) && (SceneManager.GetActiveScene().buildIndex <= last_play_scene)) 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

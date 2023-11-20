using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    private GameObject gameOverScreen;
    private Image pauseIcon;

    private bool paused;


    void Start()
    {// Start is called before the first frame update

        //get components
        gameOverScreen = GameObject.Find("UI/Canvas/Game Over");
        pauseIcon = GameObject.Find("UI/Canvas/HUD/Pause").GetComponent<Image>();

        //initialize states
        gameOverScreen.SetActive(false);
        Pause(false);
    }


    public void GameOver()
    {
        Pause(true);
        gameOverScreen.SetActive(true);
    }

    public void Pause(bool pause)
    {
        if (pause == true)
        {
            paused = true;
            Time.timeScale = 0;
            pauseIcon.enabled = true;
        }
        else
        {
            paused = false;
            Time.timeScale = 1;
            pauseIcon.enabled = false;
        }
    }

    public void Respawn()
    {//reload level
        SceneManager.LoadScene("Asteroid");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public bool Paused()
    {
        return paused;
    }
}

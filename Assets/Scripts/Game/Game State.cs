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

        Initialize();
    }

    private void Initialize()
    {//initialize states
        DisplayGameOver(false);
    }

    public void GameOver()
    {
        DisplayGameOver(true);
    }

    private void DisplayGameOver(bool gameover)
    {
        if (gameover == true)
        {
            Pause(true);
            gameOverScreen.SetActive(true);
        }
        else
        {
            Pause(false);
            gameOverScreen.SetActive(false);
        }
    }
    public void Pause(bool pause)
    {//pause/unpause game

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
    {
        

        //get current scene
        string scene = SceneManager.GetActiveScene().name;

        if (scene == "Asteroid")
        {
            Asteroid asteroid = GameObject.Find("Level").GetComponent<Asteroid>();
            asteroid.ReSpawn();
        }
    }

    public void Quit()
    {//Close game
        Application.Quit();
    }

    public bool Paused()
    {
        return paused;
    }
}

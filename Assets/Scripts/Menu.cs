using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MenuBackground;
    public GameObject pauseMenu;
    public GameObject GameOverMenu;
    public PlayerCombat playerCombat;
    public ThirdPersonController TPC;
    public ThirdPersonShooterController TPSC;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        MenuBackground.SetActive(false);
        pauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        MenuBackground.SetActive(true);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        playerCombat.enabled = false;
        TPC.LockCameraPosition = true;
        TPSC.enabled = false;
        isPaused = true;
    }
    public void ResumeGame()
    {
        MenuBackground.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        playerCombat.enabled = true;
        TPC.LockCameraPosition = false;
        TPSC.enabled = true;
        isPaused = false;
    }
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}

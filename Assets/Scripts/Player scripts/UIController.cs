using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject hudUI = null;
    [SerializeField] private GameObject endUI = null;
    [SerializeField] private GameObject pauseUI = null;

    private bool isPaused = false;

    private CameraController cameraController = null; 
    [SerializeField] private PlayerStats stats = null;

    private void Start()
    {
        SetHUD(true);
        cameraController = GetComponentInChildren<CameraController>();
        stats = GetComponent<PlayerStats>();
    }   

    public void SetHUD(bool state)
    {
            hudUI.SetActive(state);
            endUI.SetActive(!state);
            if(!stats.IsDead())
            {
                pauseUI.SetActive(!state);
            }
    }

    public void SetPause(bool state)
    {
        pauseUI.SetActive(state);
        hudUI.SetActive(!state);

        if(state)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        isPaused = state;
    }

    private void Update()
    {
        if(!stats.IsDead())
        {
            if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
            {
                SetPause(true);
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
            {
                SetPause(false);
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

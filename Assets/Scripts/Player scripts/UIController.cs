using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject hudUI = null;
    [SerializeField] private GameObject endUI = null;

    private void Start()
    {
        SetHUD(true);
    }   

    public void SetHUD(bool state)
    {
            hudUI.SetActive(state);
            endUI.SetActive(!state);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

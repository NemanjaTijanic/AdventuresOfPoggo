using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pauseUI;

    private bool pause = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        pause = !pause;
        Time.timeScale = pause ? 0 : 1;
        pauseUI.SetActive(pause);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

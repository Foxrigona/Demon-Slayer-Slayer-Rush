using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DisplayInstructions()
    {
        GameObject.Find("Instructions").GetComponent<Canvas>().sortingOrder = 1;
    }

    public void HideInstructions()
    {
        GameObject.Find("Instructions").GetComponent<Canvas>().sortingOrder = -1;
    }
}

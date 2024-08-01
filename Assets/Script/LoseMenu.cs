using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class LoseMenu : MonoBehaviour
{
    public TMP_Text scorea;
    public void Start()
    {
        scorea.text = "Score: " + PlayerPrefs.GetInt("score").ToString();
    }

    public void PlayGameAgain()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Halloween1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

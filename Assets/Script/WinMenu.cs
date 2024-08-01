using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public TMP_InputField inputName;
    public int maxScore;
    public string nameMaxScore;
    public TMP_Text score;
    public TMP_Text dong_vien;

    private void Start()
    {
        maxScore = PlayerPrefs.GetInt("score");
        score.text += maxScore;
    }
    public void save()
    {
        
        if (PlayerPrefs.GetInt("maxScore") <= PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("maxScore", maxScore);
            nameMaxScore = inputName.text;
            PlayerPrefs.SetString("nameMaxScore", nameMaxScore + " - ");
            dong_vien.text = "Chúc mừng " + nameMaxScore + " trở thành Top 1 Sever!";
        }
        else
        {
            int temp = PlayerPrefs.GetInt("maxScore") - maxScore;
            dong_vien.text = "Bạn cần " + temp.ToString() + " điểm nữa để trở thành Top 1 Sever.";
        }
    }

    public void PlayGameAgain()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void QuitGame()
    {
        
        Application.Quit();
    }


}
